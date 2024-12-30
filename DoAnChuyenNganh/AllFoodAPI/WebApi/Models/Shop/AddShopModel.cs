namespace AllFoodAPI.WebApi.Models.Shop
{
    public class AddShopModel
    {
        public int UserId { get; set; }

        public string ShopName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;
    }
}
