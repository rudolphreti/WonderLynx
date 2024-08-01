namespace App.Models
{
    public class HomeVm
    {
        public class ReferenceItemViewModel
        {
            public int ReferenceId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string? Subtitle { get; set; } = null;
            public string Type { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public string? Description { get; set; } = null;
            public string? ThumbnailUrl { get; set; } = null;
        }


        public class IndexViewModel
        {
            public List<ReferenceItemViewModel> ReferenceItems { get; set; } = [];
        }
    }
}
