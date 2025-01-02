using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int ShopId { get; set; }

    public DateOnly Date { get; set; }

    public int Total { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public bool? Status { get; set; }

    public int? VoucherId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public int OrderStatus { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Shop Shop { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
