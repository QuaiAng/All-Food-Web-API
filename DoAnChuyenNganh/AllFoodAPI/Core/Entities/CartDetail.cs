using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class CartDetail
{
    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public int Total { get; set; }

    public int ShopId { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
