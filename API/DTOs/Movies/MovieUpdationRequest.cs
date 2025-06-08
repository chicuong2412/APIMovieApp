namespace API.DTOs.Movies
{
    public class MovieUpdationRequest
    {
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public bool? Status { get; set; }
        public bool? HasSeason { get; set; }
        public string? BackdropPath { get; set; }
        public string? PosterPath { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Budget { get; set; }
        public decimal? VoteCount { get; set; }
        public string? Path { get; set; }

        public decimal? VoteAverage { get; set; }

    }
}
