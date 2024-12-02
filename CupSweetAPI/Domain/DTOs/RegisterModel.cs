using System.ComponentModel.DataAnnotations;

namespace CupcakeStoreAPI.Domain.DTOs
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")] public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")] public string? Password { get; set; }
        [Required(ErrorMessage = "UserName is required")] public string? UserName { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required.")] public string? PhoneNumber { get; set; }
        public string? ImagemUrl { get; set; }
    }
}
