using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public int ShopId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Discount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Quantity { get; set; }

    public int PaymentMethod { get; set; }

    public bool? Status { get; set; }

    public virtual Shop Shop { get; set; } = null!;
}
