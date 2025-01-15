namespace AllFoodAPI.WebApi.Models.Cart
{
    public class AddCartDetailModel
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int ShopId { get; set; }

    }
}
