namespace API.Models
{
    public class Season
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        public int SeasonNumber { get; set; }

        public int? MovieId { get; set; }

        public Movie Movie { get; set; }

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();

        public bool IsDeleted { get; set; } = false;

    }
}
