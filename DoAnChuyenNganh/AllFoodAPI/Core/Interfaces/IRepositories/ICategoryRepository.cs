using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<IEnumerable<Category>> GetCategoriesByShopId(int shopId);
        Task<bool> AddCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(Category category);
        Task<bool> ShopHasCategoryName(string categoryName, int shopId);

    }
}
