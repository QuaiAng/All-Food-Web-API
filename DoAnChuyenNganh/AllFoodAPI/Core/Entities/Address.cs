using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Address
{
    public int AddressId { get; set; }

    public int UserId { get; set; }

    public string Address1 { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
