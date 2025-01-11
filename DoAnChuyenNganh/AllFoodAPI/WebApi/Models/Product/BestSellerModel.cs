namespace AllFoodAPI.WebApi.Models.Product
{
    public class BestSellerModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Price { get; set; }

        public string? Description { get; set; }

        public int ShopId { get; set; }

        public int CategoryId { get; set; }

        public int? SalesCount { get; set; }

        public int Available { get; set; }

        public int? Rating { get; set; }
        public string ShopAddress { get; set; } = null!;
       

    }
}
