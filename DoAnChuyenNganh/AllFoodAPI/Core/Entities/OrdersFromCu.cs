using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class OrdersFromCu
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateOnly Date { get; set; }

    public int Total { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public bool Status { get; set; }

    public int ShopId { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}
