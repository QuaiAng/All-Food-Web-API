using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly Date { get; set; }

    public int Total { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public bool? Status { get; set; }

    public sbyte PaymentMethod { get; set; }

    public int OrderStatus { get; set; }

    public decimal Discount { get; set; }

    public string FullNameUser { get; set; } = null!;

    public string ShopName { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
