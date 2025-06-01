using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;
        public bool HasSeason { get; set; } = false;
        public string BackdropPath { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public decimal Revenue { get; set; } = 0;
        public decimal Budget { get; set; } = 0;
        public int Runtime { get; set; } = 0;
        public decimal VoteCount { get; set; } = 0;
        public List<Genere> Generes { get; set; } = new List<Genere>();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<ProductionCompany> ProductionCompanies { get; set; } = new List<ProductionCompany>();
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();
        public String? path { get; set; }

        public ICollection<Season> Seasons { get; set; } = new List<Season>();

        public bool IsDeleted { get; set; } = false;
    }
}