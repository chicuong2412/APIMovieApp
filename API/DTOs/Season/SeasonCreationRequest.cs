using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Season
{
    public class SeasonCreationRequest
    {
        public int SeasonNumber { get; set; } = 0;
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public string? ImagePath { get; set; }
    }
}
