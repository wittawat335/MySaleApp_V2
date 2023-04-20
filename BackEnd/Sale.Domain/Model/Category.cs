using System;
using System.Collections.Generic;

namespace Sale.Domain.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
