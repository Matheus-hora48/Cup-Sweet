using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CupcakeStoreAPI.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(400)]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        [StringLength(300)] public string? ImagemUrl { get; set; }
    }
}
