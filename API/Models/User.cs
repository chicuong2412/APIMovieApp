using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {

        public string? Name { get; set; } = string.Empty;

        public int? Age { get; set; } = 0;

        public DateTime? DoB {  get; set; } = DateTime.Now;

        public string? Address { get; set; } = string.Empty;

        //public override string? PhoneNumber { get; set; } = string.Empty;

        //public string? ProfilePicture { get; set; } = string.Empty;
        
        public override string? UserName { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public ICollection<Role> Roles { get; set; } = new List<Role>();


        public PasswordResetCode PasswordResetCode { get; set; }

    }
}
