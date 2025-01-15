using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await _service.GetAllProducts();
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpGet("Get{n:int}HighestProducts")]
        public async Task<IActionResult> GetNHighestProducts(int n)
        {
            try
            {
                var result = await _service.GetNHighestProducts(n);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("productId={id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
            try
            {
                var product = await _service.GetProductById(id);
                if (product == null) return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("categoryId={categoryId:int}")]
        public async Task<ActionResult<ProductDTO>> GetProductByCategoryId(int categoryId)
        {
            if (categoryId == 0) return BadRequest(new { success = false, message = "Category ID không hợp lệ" });
            try
            {
                var product = await _service.GetProductByCategoryId(categoryId);
                if (product == null) return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("name={name}")]
        public async Task<ActionResult<ResponseSearch>> GetProductByName(string name="")
        {
            if (string.IsNullOrEmpty(name)) return BadRequest(new { success = false, message = "Tên không hợp lệ" });
            try
            {
                var product = await _service.GetProductsByName(name);
                if (product == null) return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });
                return Ok(new { success = true, data = product });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] AddProductModel product)
        {

            try
            {
                if (product.ShopId == 0) 
                    return BadRequest(new { success = false, message = "Shop ID không hợp lệ" });
                if (product.CategoryId == 0) 
                    return BadRequest(new { success = false, message = "Category ID không hợp lệ" });
                if (product.Available < 1) 
                    return BadRequest(new { success = false, message = "Số lượng còn không hợp lệ" });
                if (product.Price < 0)
                    return BadRequest(new { success = false, message = "Giá không hợp lệ" });
                if (string.IsNullOrEmpty(product.ProductName)) 
                    return BadRequest(new { success = false, message = "Tên sản phẩm không được rỗng" });
                if (string.IsNullOrEmpty(product.Description)) 
                    return BadRequest(new { success = false, message = "Mô tả sản phẩm không được rỗng" });

                var result = await _service.AddProduct(product);
                return result ? Ok(new { success = true, message = "Thêm thành công" }) : StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Thêm thất bại" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("remove/{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteProduct(id);
                return result
                    ? Ok(new { success = true, message = "Xóa thành công" })
                    : StatusCode(StatusCodes.Status500InternalServerError, new

                    {
                        success = false,
                        message = "Thêm thất bại"
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update/shopid={shopId:int}/productId={productId:int}")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateProductModel product, int shopId, int productId)
        {
            try
            {
                if (shopId == 0)
                    return BadRequest(new { success = false, message = "Shop ID không hợp lệ" });
                if (product.CategoryId == 0)
                    return BadRequest(new { success = false, message = "Category ID không hợp lệ" });
                if (product.Available < 1)
                    return BadRequest(new { success = false, message = "Số lượng còn không hợp lệ" });
                if (product.Price < 0)
                    return BadRequest(new { success = false, message = "Giá không hợp lệ" });
                if (string.IsNullOrEmpty(product.ProductName))
                    return BadRequest(new { success = false, message = "Tên sản phẩm không được rỗng" });
                if (string.IsNullOrEmpty(product.Description))
                    return BadRequest(new { success = false, message = "Mô tả sản phẩm không được rỗng" });
                var result = await _service.UpdateProduct(product, productId, shopId);
                return result ? Ok(new
                {
                    success = true,
                    message = "Cập nhật thành công"
                }) : StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = "Xảy ra lỗi"
                });
            }
            catch (DuplicateException ex)
            {
                return BadRequest(new
                {

                    success = false,
                    field = ex.Field,
                    message = ex.Message

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }
    }
}
