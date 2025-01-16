using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Cart;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface ICartService
    {
        Task<CartDTO?> GetCartByUserId(int userId);
        Task<bool> AddCart(AddCartModel cart);
    }
}
