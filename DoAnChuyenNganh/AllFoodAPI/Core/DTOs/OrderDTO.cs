using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateOnly Date { get; set; }

        public int Total { get; set; }

        public string DeliveryAddress { get; set; } = null!;

        public bool? Status { get; set; }

        public sbyte PaymentMethod { get; set; }

        public int OrderStatus { get; set; }

        public decimal Discount { get; set; }

        public string FullNameUser { get; set; } = null!;

        public string ShopName { get; set; } = null!;

        public string PhoneNum { get; set; } = null!;
        public int UserId { get; set; }
        public virtual ICollection<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();




        public static OrderDTO FromEntity(Order order) => new OrderDTO
        {
            OrderId = order.OrderId,
            Date = order.Date,
            Total = order.Total,
            DeliveryAddress = order.DeliveryAddress,
            Status = order.Status,
            PaymentMethod = order.PaymentMethod,
            OrderStatus = order.OrderStatus,
            Discount = order.Discount,
            FullNameUser = order.FullNameUser,
            ShopName = order.ShopName,
            PhoneNum = order.PhoneNum,
            UserId = order.UserId,
            OrderDetails = order.OrderDetails
                   .Select(u => OrderDetailDTO.FromEntity(u))
                   .ToList()
        };

    }
}
