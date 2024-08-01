using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ReferenceItem
{
    public int ReferenceId { get; set; }

    public string Title { get; set; } = null!;

    public string? Subtitle { get; set; }

    public int? TypeId { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Type? Type { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
