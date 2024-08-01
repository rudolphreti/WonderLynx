namespace API.DTOs
{
    public class ReferenceItemDto
    {
        public int ReferenceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; } = null;
        public string TypeName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public string? ThumbnailUrl { get; set; } = null;
        public List<string> Tags { get; set; } = [];
    }
}