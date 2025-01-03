using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserId(int userId);
    }
}
