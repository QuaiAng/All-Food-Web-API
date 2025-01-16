namespace AllFoodAPI.WebApi.Models.Order
{
    public class AddOrderModel
    {
        public int UserId { get; set; }

        public int Total { get; set; }

        public string DeliveryAddress { get; set; } = null!;

        public sbyte PaymentMethod { get; set; }

        public decimal Discount { get; set; }

        public string FullNameUser { get; set; } = null!;

        public string ShopName { get; set; } = null!;

        public string PhoneNum { get; set; } = null!;

    }
}
