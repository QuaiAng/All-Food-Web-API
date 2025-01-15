using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IService;
using NuGet.Protocol.Core.Types;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AllFoodAPI.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                if (await ShopHasCategoryName(categoryDTO.CategoryName, categoryDTO.ShopId)) throw new DuplicateException("Shop ID", $"Shop có ID {categoryDTO.ShopId} đã tồn tại loại {categoryDTO.CategoryName}");
                return await _repository.AddCategory(CategoryDTO.ToEntity(categoryDTO));
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                if (await _repository.GetCategoryById(id) == null)
                    throw new DuplicateException("ID", $"Không tồn tại loại có ID {id}");
                return await _repository.DeleteCategory(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            try
            {
                var categories = await _repository.GetAllCategories();
                var categoryDTOs = categories.Select(u => CategoryDTO.FromEntity(u));
                return categoryDTOs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByShopId(int shopId)
        {
            try
            {
                var categories = await _repository.GetCategoriesByShopId(shopId);
                var categoryDTOs = categories.Select(u => CategoryDTO.FromEntity(u));
                return categoryDTOs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            try
            {
                var category = await _repository.GetCategoryById(id);
                if (category == null) return null;
                return CategoryDTO.FromEntity(category);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> ShopHasCategoryName(string categoryName, int shopId)
        {
            return await _repository.ShopHasCategoryName(categoryName, shopId);
        }

        public async Task<bool> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _repository.GetCategoryById(categoryDTO.CategoryId);
                if (category == null) throw new DuplicateException("CategoryID", $"Không tồn tại ID {categoryDTO.CategoryId}");
                if (await ShopHasCategoryName(categoryDTO.CategoryName, categoryDTO.ShopId)) throw new DuplicateException("Shop ID", $"Shop có ID {categoryDTO.ShopId} đã tồn tại loại {categoryDTO.CategoryName}");
                category.CategoryName = categoryDTO.CategoryName;
                return await _repository.UpdateCategory(category);
            }
            catch
            {
                throw;
            }
        }
    }
}
