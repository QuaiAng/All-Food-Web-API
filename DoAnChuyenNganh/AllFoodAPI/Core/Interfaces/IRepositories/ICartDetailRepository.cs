using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface ICartDetailRepository
    {
        Task<bool> AddCartDetail(CartDetail cartDetail);
        Task<bool> DeteleCartDetail(int productId, int cartId);
        Task<bool> UpdateCartDetail(CartDetail cartDetail);
        Task<bool> IsExistProductInCart(int productId, int cartId);
        Task<CartDetail?> GetCartDetailByProductId (int productId, int cartId);
    }
}
