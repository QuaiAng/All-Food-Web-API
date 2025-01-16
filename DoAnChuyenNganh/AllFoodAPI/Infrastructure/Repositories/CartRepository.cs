using System.Net;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AllfoodDbContext _context;

        public CartRepository(AllfoodDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCart(Cart cart)
        {
            try
            {
                _context.Carts.Add(cart);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<Cart?> GetCartByUserId(int userId)
        {
            try
            {
                var cart = await _context.Carts
                                         .Where(u => u.UserId == userId)
                                         .Include(c => c.CartDetails) // Bao gồm CartDetails
                                             .ThenInclude(cd => cd.Product) // Bao gồm Product thông qua CartDetails
                                         .Include(c => c.CartDetails)
                                             .ThenInclude(cd => cd.Shop) // Bao gồm Shop thông qua CartDetails
                                         .SingleOrDefaultAsync();

                if (cart == null)
                    return null;
                return cart;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> IsUserHasCart(int userId)
        {
            return await _context.Carts.Where(u => u.UserId == userId).CountAsync() > 0;
        }
    }
}
