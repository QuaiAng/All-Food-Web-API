using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class VoucherDTO
    {
        public int VoucherId { get; set; }

        public int ShopId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int Discount { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public int Quantity { get; set; }

        public int PaymentMethod { get; set; }


        public static VoucherDTO FromEntity(Voucher voucher) => new VoucherDTO
        {
            VoucherId = voucher.VoucherId,
            ShopId = voucher.ShopId,
            Title = voucher.Title,
            Description = voucher.Description,
            Discount = voucher.Discount,
            StartDate = voucher.StartDate,
            EndDate = voucher.EndDate,
            Quantity = voucher.Quantity,
            PaymentMethod = voucher.PaymentMethod,

        };

        public static Voucher ToEntity(VoucherDTO voucher) => new Voucher
        {
            VoucherId = voucher.VoucherId,
            ShopId = voucher.ShopId,
            Title = voucher.Title,
            Description = voucher.Description,
            Discount = voucher.Discount,
            StartDate = voucher.StartDate,
            EndDate = voucher.EndDate,
            Quantity = voucher.Quantity,
            PaymentMethod = voucher.PaymentMethod,

        };

    }
}
