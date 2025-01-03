using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class CartDTO
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int Total { get; set; }

        public virtual ICollection<CartDetailDTO> CartDetails { get; set; } = new List<CartDetailDTO>();


        public static CartDTO FromEntity(Cart cart) => new CartDTO
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            Total = cart.Total,
            CartDetails = cart.CartDetails
                   .Select(u => CartDetailDTO.FromEntity(u))
                   .ToList()
        };
        
        public static Cart ToEntity(CartDTO cart) => new Cart
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            Total = cart.Total,
            CartDetails = cart.CartDetails
                   .Select(CartDetailDTO.ToEntity)
                   .ToList()
        };
    }
}
