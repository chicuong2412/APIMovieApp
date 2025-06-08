using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Movies
{
    public class MovieCreationRequest
    {
        [Required]
        public string Title { get; set; }
        public string Overview { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;
        public bool HasSeason { get; set; } = false;
        public string BackdropPath { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public decimal Revenue { get; set; } = 0;
        public decimal Budget { get; set; } = 0;
        public decimal VoteCount { get; set; } = 0;
        public string Path { get; set; } = string.Empty;
        public decimal VoteAverage { get; set; } = 0;

        //public List<string> Genres { get; set; }
    }
}
