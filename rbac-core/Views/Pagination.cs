using System.ComponentModel.DataAnnotations;

namespace rbac_core.Views
{
    public sealed class SortRequest
    {
        [RegularExpression(
            "^(asc|desc)$",
            ErrorMessage = "Sort direction must be 'asc' or 'desc'."
        )]
        public string? Direction { get; set; } = "asc";

        public string? Field { get; set; }
    }

    public sealed class PageRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be at least 1.")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;

        public bool? AllRecords { get; set; }
    }

    public sealed class PaginatedRequest<T>
    {
        [Required]
        public required PageRequest PageRequest { get; set; }

        public SortRequest? SortRequest { get; set; }

        [Required]
        public required T Filters { get; set; }
    }

    public sealed class PaginatedResponse<T>(int totalRecords, List<T> data)
    {
        public List<T> Data { get; set; } = data;
        public int TotalRecords { get; set; } = totalRecords;
    }
}
