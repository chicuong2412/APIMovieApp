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
    }


}
