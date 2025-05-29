using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Authentication
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RequestToken { get; set; }
    }
}
