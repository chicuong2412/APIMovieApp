namespace API.DTOs.Season
{
    public class SeasonUpdationRequest
    {
        public int? SeasonNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
