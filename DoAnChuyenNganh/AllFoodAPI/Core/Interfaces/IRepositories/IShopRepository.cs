using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepository
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShops();
        Task<Shop?> GetShopById(int shopId);
        Task<bool> AddShop(Shop shop);
        Task<bool> UpdateShop(Shop shop);
        Task<bool> DeleteShop(int shopId);
        Task<bool> UserHasShop(int UserId);
    }
}
