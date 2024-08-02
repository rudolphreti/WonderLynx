using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace App.Models
{
    public class HomeVm
    {
        public class ReferenceItemViewModel
        {
            public int ReferenceId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string? Subtitle { get; set; } = null;
            public string TypeName { get; set; } = string.Empty;
            public string CategoryName { get; set; } = string.Empty;
            public string? Description { get; set; } = null;
            public string? ThumbnailUrl { get; set; } = null;
            public List<string> Tags { get; set; } = [];
            public List<string> Categories { get; set; } = [];
            public List<string> Types { get; set; } = [];
        }


        public class IndexViewModel
        {
            public List<ReferenceItemViewModel> ReferenceItems { get; set; } = [];
        }
    }
}
