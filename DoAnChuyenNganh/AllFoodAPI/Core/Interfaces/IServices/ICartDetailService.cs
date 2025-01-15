using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Cart;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface ICartDetailService
    {
        Task<bool> AddCartDetail(AddCartDetailModel cartDetail);
        Task<bool> UpdateCartDetail(int quantity, int productId, int cartId);
        Task<bool> DeleteCartDetail(int productId, int cartId);
    }
}
