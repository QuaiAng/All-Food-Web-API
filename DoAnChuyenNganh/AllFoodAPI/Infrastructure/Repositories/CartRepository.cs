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
        public async Task<Cart?> GetCartByUserId(int userId)
        {
            try
            {
                var cart = await _context.Carts.Where(u => u.UserId == userId).Include(c => c.CartDetails).SingleOrDefaultAsync();
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
    }
}
