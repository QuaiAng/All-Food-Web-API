using System;
using System.Collections.Generic;

namespace AllFoodAPI.Core.Entities;

public partial class Image
{
    public int ImageId { get; set; }

    public int ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
