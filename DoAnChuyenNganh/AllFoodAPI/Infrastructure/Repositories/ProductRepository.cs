using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AllfoodDbContext _context;

        public ProductRepository(AllfoodDbContext context)
        {
            _context = context;            
        }
        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync(u => u.ProductId == id);
                if (product == null) return false;
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<Product?> GetProductById(int id)
        {
            try
            {
                var product = await _context.Products.SingleOrDefaultAsync(u => u.ProductId == id);

                if (product == null)
                {
                    return null;
                }

                return product;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> IsCategoryExist(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }

        public async Task<bool> IsShopExist(int shopId)
        {
            return await _context.Shops.AnyAsync(c => c.ShopId == shopId);

        }

        public async Task<bool> ShopHasProductName(string productName, int shopId, int productId = 0)
        {
            if(productId == 0) 
                return await _context.Products.AnyAsync(u => u.ProductName == productName && u.ShopId == shopId);
            return await _context.Products.AnyAsync(u => u.ProductName == productName && u.ShopId == shopId && u.ProductId != productId);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var productUpdate = await _context.Products.SingleOrDefaultAsync(u => u.ProductId == product.ProductId);
                if (productUpdate == null)
                {
                    return false;
                }
                _context.Products.Update(productUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
