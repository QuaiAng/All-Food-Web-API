using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IService;
using AllFoodAPI.WebApi.Models.Shop;

namespace AllFoodAPI.Application.Service
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _repository;
        private readonly IUserRepository _userRepository;

        public ShopService(IShopRepository repository, IUserRepository userRepository) {
        
            _repository = repository;
            _userRepository = userRepository;

        }
        public async Task<bool> AddShop(AddShopModel shopModel)
        {
            try
            {
                if (await _userRepository.GetUserById(shopModel.UserId) == null) throw new DuplicateException("User ID", "User ID không tồn tại");
                if (await UserHasShop(shopModel.UserId)) throw new DuplicateException("Shop", "User này đã đăng ký shop");
                var shop = new Shop
                {
                    ShopId = 0,
                    ShopName = shopModel.ShopName,
                    Address = shopModel.Address,
                    Phone = shopModel.Phone,
                    Rating = 0,
                    Status = true,
                    UserId = shopModel.UserId,

                };
                return await _repository.AddShop(shop);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<bool> DeleteShop(int shopId)
        {
            try
            {
                var address = await _repository.GetShopById(shopId);
                if (address == null) throw new DuplicateException("Shop", "Không tìm shop này");
                return await _repository.DeleteShop(shopId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ShopDTO>> GetAllShops()
        {
            try
            {
                var shops = await _repository.GetAllShops();
                var shopDTOs = shops.Select(u => ShopDTO.FromEntity(u));
                return shopDTOs;
            }
            catch { throw; }
        }

        public async Task<ShopDTO?> GetShopById(int shopId)
        {
            try
            {
                var shop = await _repository.GetShopById(shopId);
                if (shop == null)
                {
                    return null;
                }
                var shopDTO = ShopDTO.FromEntity(shop);
                return shopDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<ShopDTO?> GetShopByUserId(int userId)
        {
            try
            {
                var shop = await _repository.GetShopByUserId(userId);
                if (shop == null)
                {
                    return null;
                }
                var shopDTO = ShopDTO.FromEntity(shop);
                return shopDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> UpdateShop(UpdateShopModel shopModel, int id)
        {
            try
            {
                if (await _userRepository.GetUserById(id) == null) throw new DuplicateException("User ID", "User ID không tồn tại");
                var shopUpdate = await _repository.GetShopById(id);
                if (shopUpdate == null)
                {
                    return false;
                }
                shopUpdate.Phone = shopModel.Phone;
                shopUpdate.Address = shopModel.Address;
                shopUpdate.ShopName = shopModel.ShopName;
                return await _repository.UpdateShop(shopUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<bool> UserHasShop(int UserId)
        {
            if (UserId == 0) return true;
            return await _repository.UserHasShop(UserId);
        }
    }
}
