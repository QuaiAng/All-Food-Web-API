using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface ICartDetailService
    {
        Task<bool> AddCartDetail(CartDetailDTO cartDetail);
        Task<bool> DeteleCartDetail(int id);
        Task<bool> UpdateCartDetail(int quantity, int id);
    }
}
