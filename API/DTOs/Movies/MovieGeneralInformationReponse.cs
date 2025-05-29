using API.Models;

namespace API.DTOs.Movies
{
    public class MovieGeneralInformationReponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string PosterPath { get; set; } = string.Empty;
        public string BackdropPath { get; set; } = string.Empty;
        public double VoteAverage { get; set; }
        public decimal VoteCount { get; set; }
        public bool HasSeason { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
        public string? path { get; set; }
    }
}
