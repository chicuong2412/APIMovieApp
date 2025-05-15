using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LogoutJWT
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
}
