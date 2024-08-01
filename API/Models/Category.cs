using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReferenceItem> ReferenceItems { get; set; } = new List<ReferenceItem>();
}
