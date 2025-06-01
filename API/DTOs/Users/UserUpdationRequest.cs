using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Users
{
    public class UserUpdationRequest
    {
        public string? Username { get; set; }

        public string? Name { get; set; }

        public int? Age { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime? DoB { get; set; }

        [Phone]
        public String? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
