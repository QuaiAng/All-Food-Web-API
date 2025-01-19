using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Shop
{
    public int ShopId { get; set; }

    public int UserId { get; set; }

    public string ShopName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public double Rating { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
