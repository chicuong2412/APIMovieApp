using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Role : IdentityRole
    {
        public Role() : base()
        {
        }
        public Role(string roleName) : base(roleName)
        {
        }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();


        public ICollection<User> Users { get; set; } = new List<User>();

    }


}
