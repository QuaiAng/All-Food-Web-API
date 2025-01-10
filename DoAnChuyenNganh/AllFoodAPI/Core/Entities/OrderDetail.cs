using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public string? Note { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
