using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly AllfoodDbContext _context;

        public CartDetailRepository(AllfoodDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> AddCartDetail(CartDetail cartDetail)
        {
            try
            {
                _context.CartDetails.Add(cartDetail);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeteleCartDetail(int id)
        {
            try
            {
                var cartDetail = await _context.CartDetails.SingleOrDefaultAsync(u => u.ProductId == id);
                if (cartDetail == null) return false;
                _context.CartDetails.Remove(cartDetail);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartDetail?> GetCartDetailByProductId(int productId)
        {
            try
            {
                var cartDetail = await _context.CartDetails.SingleOrDefaultAsync(u => u.ProductId == productId);
                if (cartDetail == null) return null;
                return cartDetail;
            }
            catch (Exception ex) {
            
                throw new Exception(ex.Message);

            }
        }

        public async Task<bool> UpdateCartDetail(CartDetail cartDetail)
        {
            try
            {
                var cartDetailUpdate = await _context.CartDetails.SingleOrDefaultAsync(u => u.ProductId == cartDetail.ProductId);
                if (cartDetailUpdate == null) return false;
                _context.CartDetails.Remove(cartDetailUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
