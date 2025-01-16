using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class OrderDetailDTO
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public string? Note { get; set; }

        public string ProductName { get; set; } = null!;

        public static OrderDetailDTO FromEntity(OrderDetail orderDetai) => new OrderDetailDTO
        {
            OrderId = orderDetai.OrderId,
            Price = orderDetai.Price,
            ProductId = orderDetai.ProductId,
            Quantity = orderDetai.Quantity,
            Note = orderDetai.Note,
            ProductName = orderDetai.ProductName,
        };
    }
}
