using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Shop;

namespace AllFoodAPI.Core.Interfaces.IService
{
    public interface IShopService
    {
        Task<IEnumerable<ShopDTO>> GetAllShops();
        Task<ShopDTO?> GetShopById(int shopId);
        Task<bool> AddShop(AddShopModel shopModel);
        Task<bool> UpdateShop(UpdateShopModel shopModel, int id);
        Task<bool> DeleteShop(int shopId);
        Task<bool> UserHasShop(int UserId);
        Task<ShopDTO?> GetShopByUserId(int userId);

    }
}
