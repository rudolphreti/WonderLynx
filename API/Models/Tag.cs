using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReferenceItem> References { get; set; } = new List<ReferenceItem>();
}
