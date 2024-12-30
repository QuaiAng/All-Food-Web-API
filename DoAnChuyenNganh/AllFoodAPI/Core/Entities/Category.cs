using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public int ShopId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Shop Shop { get; set; } = null!;
}
