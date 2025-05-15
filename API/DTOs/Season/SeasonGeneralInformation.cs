

namespace API.DTOs.Season
{
    public class SeasonGeneralInformation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int SeasonNumber { get; set; }
    }
}
