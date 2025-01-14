using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class CartDetailDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int Total { get; set; }

        //public string

        public int ShopId { get; set; }

        public static CartDetailDTO FromEntity(CartDetail cartDetail) => new CartDetailDTO
        {
            ProductId = cartDetail.ProductId,
            Quantity = cartDetail.Quantity,
            Price = cartDetail.Price,
            Total = cartDetail.Total,
            ShopId = cartDetail.ShopId
        };

        public static CartDetail ToEntity(CartDetailDTO cartDetail) => new CartDetail
        {
            ProductId = cartDetail.ProductId,
            Quantity = cartDetail.Quantity,
            Price = cartDetail.Price,
            Total = cartDetail.Total,
            ShopId = cartDetail.ShopId
        };
    }
}
