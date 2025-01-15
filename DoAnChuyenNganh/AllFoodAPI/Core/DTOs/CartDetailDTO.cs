using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class CartDetailDTO
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string ShopName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProductImage { get; set; } = null!;
        public int ShopId { get; set; }

        public static CartDetailDTO FromEntity(CartDetail cartDetail) => new CartDetailDTO
        {
            ProductId = cartDetail.ProductId,
            CartId = cartDetail.CartId,
            Quantity = cartDetail.Quantity,
            Price = cartDetail.Product.Price,
            ShopId = cartDetail.ShopId,
            ShopName = cartDetail.Shop.ShopName,
            ProductName = cartDetail.Product.ProductName.Trim(),
            Description = cartDetail.Product.Description!,
            ProductImage = cartDetail.Product.ImageUrl!
        };

        public static CartDetail ToEntity(CartDetailDTO cartDetail) => new CartDetail
        {
            ProductId = cartDetail.ProductId,
            Quantity = cartDetail.Quantity,
            ShopId = cartDetail.ShopId,

        };
    }
}
