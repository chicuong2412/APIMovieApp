namespace API.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string PosterPath {  get; set; } = string.Empty;
        public int EpisodeNumber { get; set; }
        public int? SeasonId { get; set; }
        public Season Season { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
