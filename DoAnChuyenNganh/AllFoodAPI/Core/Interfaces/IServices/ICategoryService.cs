using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AllFoodAPI.Core.Interfaces.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategories();
        Task<CategoryDTO?> GetCategoryById(int id);
        Task<IEnumerable<CategoryDTO>> GetCategoriesByShopId(int shopId);
        Task<bool> AddCategory(CategoryDTO categoryDTO);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(CategoryDTO categoryDTO);
        Task<bool> ShopHasCategoryName(string categoryName, int shopId);

    }
}
