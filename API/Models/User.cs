using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {

        public string Name { get; set; } = string.Empty.ToString();

        public int? Age { get; set; } = 0;

        public DateTime? DoB {  get; set; } = DateTime.Now;

        public string Address { get; set; } = string.Empty.ToString();

    }
}
