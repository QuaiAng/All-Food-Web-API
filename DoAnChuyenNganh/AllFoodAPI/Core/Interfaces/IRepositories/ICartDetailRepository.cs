using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface ICartDetailRepository
    {
        Task<bool> AddCartDetail(CartDetail cartDetail);
        Task<bool> DeteleCartDetail(int id);
        Task<bool> UpdateCartDetail(CartDetail cartDetail);
        Task<CartDetail?> GetCartDetailByProductId (int productId);
    }
}
