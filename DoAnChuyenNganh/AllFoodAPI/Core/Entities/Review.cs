using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string? Comment { get; set; }

    public double Rating { get; set; }

    public DateOnly Date { get; set; }

    public bool? Status { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
