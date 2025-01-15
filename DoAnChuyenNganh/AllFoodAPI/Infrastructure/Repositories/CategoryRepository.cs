using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AllfoodDbContext _context;

        public CategoryRepository(AllfoodDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);

            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var category = await _context.Categories.SingleOrDefaultAsync(u => u.CategoryId == id);
                if (category == null) return false;
                _context.Categories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Xảy ra lỗi khi xóa", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetCategoriesByShopId(int shopId)
        {
            try
            {
                var categories = await _context.Categories.Where(u => u.ShopId == shopId).ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            try
            {
                var category = await _context.Categories.SingleOrDefaultAsync(u => u.CategoryId == id);
                if (category == null) return null;
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> ShopHasCategoryName(string categoryName, int shopId)
        {
            return await _context.Categories.AnyAsync(u => u.CategoryName == categoryName && u.ShopId == shopId);
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            try
            {
                var categoryUpdate = await _context.Categories.SingleOrDefaultAsync(u => u.CategoryId == category.CategoryId);
                if (categoryUpdate == null) return false;
                categoryUpdate.CategoryName = category.CategoryName;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }
    }
}
