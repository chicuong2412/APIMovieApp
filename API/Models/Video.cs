using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
