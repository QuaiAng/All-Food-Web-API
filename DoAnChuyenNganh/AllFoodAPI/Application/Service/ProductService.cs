using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.Infrastructure.Data;
using AllFoodAPI.WebApi.Models.Product;
using System.Diagnostics.Metrics;
using System.Linq;

namespace AllFoodAPI.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IShopRepository _shopRepository;

        public ProductService(IProductRepository repository, IShopRepository shopRepository)
        {
            _repository = repository;
            _shopRepository = shopRepository;
        }
        public async Task<bool> AddProduct(AddProductModel AddProduct)
        {
            try
            {
                if (await ShopHasProductName(AddProduct.ProductName, AddProduct.ShopId)) 
                    throw new DuplicateException("ProductName", $"Shop có ID {AddProduct.ShopId} đã tồn tại sản phẩm tên {AddProduct.ProductName}");
                if(await IsCategoryExist(AddProduct.CategoryId) == false)
                    throw new DuplicateException("Category", $"Loại có ID {AddProduct.CategoryId} không tồn tại");
                if(await IsShopExist(AddProduct.ShopId) == false)
                    throw new DuplicateException("Shop", $"Shop có ID {AddProduct.ShopId} không tồn tại");

                var product = new Product
                {
                    ProductName = AddProduct.ProductName,
                    Price = AddProduct.Price,
                    Description = AddProduct.Description,
                    SalesCount = 0,
                    ShopId = AddProduct.ShopId,
                    CategoryId = AddProduct.CategoryId,
                    Available = AddProduct.Available,
                    Status = true,
                };

                return await _repository.AddProduct(product);
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var user = await _repository.GetProductById(id);
                if (user == null) throw new DuplicateException("Product", "Không tìm thấy sản phẩm");
                return await _repository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");

                throw;
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            try
            {
                var products = await _repository.GetAllProducts();

                var productDTOs = products.Select(u => ProductDTO.FromEntity(u));

                return productDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetProductByCategoryId(int categoryId)
        {
            try
            {
                var products = await _repository.GetProductByCategoryId(categoryId);

                var productDTOs = products.Select(u => ProductDTO.FromEntity(u));

                return productDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<ProductDTO?> GetProductById(int id)
        {
            try
            {
                var product = await _repository.GetProductById(id);

                if (product == null)
                {
                    return null;
                }

                var productDTO = ProductDTO.FromEntity(product);

                return productDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ResponseSearch>> GetProductsByName(string name)
        {

            try
            {
               
                var products = await _repository.GetProductsByName(name);
                
                var productDTOs = products.Where(u => u.Shop.ShopId == u.ShopId).Select(u => new ResponseSearch {

                    ProductName = u.ProductName,
                    Price = u.Price,
                    ShopName = u.Shop.ShopName,
                    ProductId = u.ProductId,
                    ShopAddress = u.Shop.Address,

                });

                return productDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public Task<bool> IsCategoryExist(int categoryId)
        {
            return _repository.IsCategoryExist(categoryId);
        }

        public Task<bool> IsShopExist(int shopId)
        {
            return _repository.IsShopExist(shopId);
        }

        public Task<bool> ShopHasProductName(string productName, int shopId, int productId = 0)
        {
            return _repository.ShopHasProductName(productName, shopId);
        }

        public async Task<bool> UpdateProduct(UpdateProductModel UpdateProduct, int productId, int shopId)
        {
            try
            {
                if (await ShopHasProductName(UpdateProduct.ProductName, shopId, productId))
                    throw new DuplicateException("ProductName", $"Shop có ID {shopId} đã tồn tại sản phẩm tên {UpdateProduct.ProductName}");
                if (await IsCategoryExist(UpdateProduct.CategoryId) == false)
                    throw new DuplicateException("Category", $"Loại có ID {UpdateProduct.CategoryId} không tồn tại");
                if (await IsShopExist(shopId) == false)
                    throw new DuplicateException("Shop", $"Shop có ID {shopId} không tồn tại");

                var product = await _repository.GetProductById(productId);

                if (product == null) throw new DuplicateException("Product", $"Sản phẩm có ID {productId} không tồn tại");
                
                product.ProductName = UpdateProduct.ProductName;
                product.Price = UpdateProduct.Price;
                product.Description = UpdateProduct.Description;
                product.CategoryId = UpdateProduct.CategoryId;
                product.Available = UpdateProduct.Available;
                
                return await _repository.UpdateProduct(product);
            }
            catch
            {
                throw;
            }
        }
    }
}
