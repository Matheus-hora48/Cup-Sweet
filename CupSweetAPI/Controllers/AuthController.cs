using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CupcakeStoreAPI.Domain.DTOs;
using CupcakeStoreAPI.Domain.Models;
using CupcakeStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CupcakeStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel? model)
        {
            if (model is null) return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null || !(await _userManager.CheckPasswordAsync(user, model.Password!)))
                return Unauthorized();

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(type: "id", user.Id),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                out int refreshTokenValidityInMinutes);

            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = refreshToken,
                Expiration = token.ValidTo
            });

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel? model)
        {
            if (model is null) return BadRequest();

            var userExists = await _userManager.FindByEmailAsync(model.Email!);

            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                ImagemUrl = model.ImagemUrl
            };

            var result = await _userManager.CreateAsync(user, model.Password!);

            return !result.Succeeded ?
                StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = $"User creation failed. {Environment.NewLine}" +
                    $" {string.Join(Environment.NewLine, result.Errors.Select(e => e.Description))}"
                }) :
                Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null)
                return BadRequest("Invalid client request.");

            var accessToken = tokenModel.AccessToken ?? throw new ArgumentNullException(nameof(tokenModel));
            var refreshToken = tokenModel.RefreshToken ?? throw new ArgumentNullException(nameof(tokenModel));
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            if (principal == null)
                return BadRequest("Invalid access/refresh token.");

            string username = principal.Identity?.Name ?? string.Empty;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid access/refresh token.");

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
            var newRefreshtoken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshtoken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshtoken,
                expiration = newAccessToken.ValidTo
            });
        }

        [HttpPost]
        [Route("revoke/{email}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Revoke(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return BadRequest("Invalid user email.");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();

        }

        [HttpPost]
        [Route("create-role")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (roleExist)
                return BadRequest(new Response { Status = "Error", Message = "Role already exists" });

            var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (roleResult.Succeeded)
                return Ok(new Response { Status = "Success", Message = $"Role {roleName} added successfully" });

            return BadRequest(new Response { Status = "Error", Message = $"Issue adding the new {roleName} role." });
        }

        [HttpPost]
        [Route("add-user-to-role")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest(new Response { Status = "Error", Message = "Unable to find user." });

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                return Ok(new Response()
                {
                    Status = "Success",
                    Message = $"User {user.UserName} added to the {roleName} role."
                });

            return BadRequest(new Response()
            {
                Status = "Error",
                Message = $"Unable to add user {user.UserName} to the {roleName} role"
            });
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser>> GetLoggedUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return BadRequest("Unable to find user.");

            return Ok(user);
        }
    }
}
