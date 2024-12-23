using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Report
{
    public int ReportId { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string Reason { get; set; } = null!;

    public DateOnly Date { get; set; }

    public bool? Status { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
