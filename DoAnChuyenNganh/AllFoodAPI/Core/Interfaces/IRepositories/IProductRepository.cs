using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId);
        Task<Product?> GetProductById(int id);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<bool> ShopHasProductName(string productName, int shopId, int productId = 0);
        Task<bool> IsShopExist(int shopId);
        Task<bool> IsCategoryExist(int categoryId);
        
    }
}
