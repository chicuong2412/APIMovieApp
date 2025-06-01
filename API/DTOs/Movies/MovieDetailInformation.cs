using API.Models;

namespace API.DTOs.Movies
{
    public class MovieDetailInformation
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
        public int Runtime { get; set; } = 0;
        public decimal Revenue { get; set; } = 0;
        public decimal Budget { get; set; } = 0;
        public bool Status { get; set; } = false;
        public List<Genere> Generes { get; set; } = new List<Genere>();
        public List<ProductionCompany> ProductionCompanies { get; set; } = new List<ProductionCompany>();
        public string? path { get; set; }
    }
}
