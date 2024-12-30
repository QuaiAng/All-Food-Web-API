using AllFoodAPI.Core.Entities;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AllFoodAPI.Core.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public int ShopId { get; set; }

        public static CategoryDTO FromEntity(Category category)
        {

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ShopId = category.ShopId,
            };
        }
        public static Category ToEntity(CategoryDTO categoryDTO)
        {

            return new Category
            {
                CategoryId = categoryDTO.CategoryId,
                CategoryName = categoryDTO.CategoryName,
                ShopId = categoryDTO.ShopId,
            };
        }
    }
}
