using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Episode
{
    public class EpisodeCreationRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Overview { get; set; }
        public string Path { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public int EpisodeNumber { get; set; }
        public int? SeasonId { get; set; }
        
    }
}
