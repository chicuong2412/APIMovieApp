using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Episode
{
    public class EpisodeUpdationRequest
    {
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? Path { get; set; }
        public string? PosterPath { get; set; }
        public int? EpisodeNumber { get; set; }

        public int? SeasonId { get; set; }
    }
}
