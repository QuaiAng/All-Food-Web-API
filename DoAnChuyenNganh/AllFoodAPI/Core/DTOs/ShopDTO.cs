using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class ShopDTO
    {
        public int ShopId { get; set; }
        public int UserId { get; set; }
        public string ShopName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ImageURL { get; set; } = null!;
        public double Rating { get; set; }

        public static ShopDTO FromEntity(Shop shop)
        {
            return new ShopDTO
            {
                ShopId = shop.ShopId,
                UserId = shop.UserId,
                Address = shop.Address,
                ShopName = shop.ShopName,
                Phone = shop.Phone,
                Rating = shop.Rating,
                ImageURL = shop.User.ImageUrl
            };
        }

    }
}
