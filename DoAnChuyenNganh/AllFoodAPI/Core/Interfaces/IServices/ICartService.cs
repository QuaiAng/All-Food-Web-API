using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface ICartService
    {
        Task<CartDTO?> GetCartByUserId(int userId);

    }
}
