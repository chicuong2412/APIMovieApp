using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Authentication
{
    public class ChangePassRequest
    {

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Token { get; set; }

    }
}
