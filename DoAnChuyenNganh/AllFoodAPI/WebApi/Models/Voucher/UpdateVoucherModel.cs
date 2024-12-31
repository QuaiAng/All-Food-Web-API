namespace AllFoodAPI.WebApi.Models.Voucher
{
    public class UpdateVoucherModel
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int Discount { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Quantity { get; set; }

        public int PaymentMethod { get; set; }
    }
}
