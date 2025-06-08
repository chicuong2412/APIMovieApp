using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Users
{
    public class ScreenTime
    {
        [Required]
        public decimal Value { get; set; } = 0;
    }
}
