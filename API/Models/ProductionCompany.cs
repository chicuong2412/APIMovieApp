namespace API.Models
{
    public class ProductionCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public bool IsDeleted { get; set; } = false;
    }
}
