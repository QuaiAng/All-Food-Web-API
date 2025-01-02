using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Product;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<IEnumerable<ProductDTO>> GetProductByCategoryId(int categoryId);
        Task<ProductDTO?> GetProductById(int id);
        Task<bool> AddProduct(AddProductModel AddProduct);
        Task<bool> UpdateProduct(UpdateProductModel UpdateProduct, int productId, int shopId);
        Task<bool> DeleteProduct(int id);
        Task<bool> ShopHasProductName(string productName, int shopId, int productId = 0);
        Task<bool> IsShopExist(int shopId);
        Task<bool> IsCategoryExist(int categoryId);

    }
}
