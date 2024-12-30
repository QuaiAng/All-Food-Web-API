namespace AllFoodAPI.WebApi.Models.Product
{
    public class AddProductModel
    {
        public string ProductName { get; set; } = null!;
        public int Price { get; set; }
        public string? Description { get; set; }
        public int ShopId { get; set; }
        public int Available { get; set; }
        public int CategoryId { get; set; }
    }
}
