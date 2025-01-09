using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly AllfoodDbContext _context;

        public ShopRepository(AllfoodDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> AddShop(Shop shop)
        {
            try
            {
                _context.Shops.Add(shop);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<bool> DeleteShop(int shopId)
        {
            try
            {
                var shop = await _context.Shops.SingleOrDefaultAsync(u => u.ShopId == shopId);
                if (shop == null) return false;
                shop.Status = false;
                _context.Shops.Update(shop);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Shop>> GetAllShops()
        {
            try
            {
                var shops = await _context.Shops.Where(u => u.Status == true).ToListAsync();
                return shops;
            }
            catch (Exception ex) {
            
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Shop?> GetShopById(int shopId)
        {
            try
            {
                var shop = await _context.Shops.SingleOrDefaultAsync(u => u.ShopId == shopId && u.Status == true);
                if (shop == null) 
                    return null;
                return shop;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Shop?> GetShopByUserId(int userId)
        {
            try
            {
                var shop = await _context.Shops.SingleOrDefaultAsync(u => u.UserId == userId);
                return shop == null ? null : shop;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Shop>> GetNHighestShops(int n)
        {
            try
            {
                var shops = await _context.Shops.OrderByDescending(u => u.Rating).Take(n).ToListAsync();
                return shops;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateShop(Shop shop)
        {
            try
            {
                var shopUpdate = await _context.Shops.SingleOrDefaultAsync(u => u.ShopId == shop.ShopId );
                if (shopUpdate == null)
                {
                    return false;
                }
                _context.Shops.Update(shopUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UserHasShop(int UserId)
        {
            return await _context.Shops.SingleOrDefaultAsync(u => u.UserId == UserId) != null;
        }
    }
}
