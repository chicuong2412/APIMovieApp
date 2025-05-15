namespace API.DTOs
{
    public class ObjectFilter
    {
        public string? Search { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;

        public string? SortBy { get; set; }

        public bool IsAscending { get; set; } = true;

    }
}
