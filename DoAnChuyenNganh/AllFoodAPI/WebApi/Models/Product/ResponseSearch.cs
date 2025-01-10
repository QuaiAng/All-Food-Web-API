namespace AllFoodAPI.WebApi.Models.Product
{
    public class ResponseSearch
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;
        public string ShopName { get; set; } = null!;
        public string ShopAddress { get; set; } = null!;
        public int Price { get; set; }
    }
}
