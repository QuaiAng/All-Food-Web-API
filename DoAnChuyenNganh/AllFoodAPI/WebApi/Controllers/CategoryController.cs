using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _service.GetAllCategories();
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
            try
            {
                var category = await _service.GetCategoryById(id);
                if (category == null) return NotFound(new { success = false, message = "Không tìm thấy loại" });
                return Ok(new { success = true, data = category });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {

            try
            {
                if (categoryDTO.ShopId == 0) return BadRequest(new { success = false, message = "Shop ID không hợp lệ" });
                if (string.IsNullOrEmpty(categoryDTO.CategoryName)) return BadRequest(new { success = false, message = "Tên loại không được rỗng" });
                var result = await _service.AddCategory(categoryDTO);
                return result ? Ok(new { success = true, message = "Thêm thành công" }) : StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Thêm thất bại" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteCategory(id);
                return result ? Ok(new { success = true, message = "Xóa thành công" }) : StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Thêm thất bại" });
            }
            catch(DuplicateException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    field = ex.Field,
                    message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryDTO.CategoryName)) return BadRequest(new { success = false, message = "Tên loại rỗng" });
                if (categoryDTO.CategoryId == 0) return BadRequest(new { success = false, message = "Category ID không hợp lệ" });
                if (categoryDTO.ShopId == 0) return BadRequest(new { success = false, message = "Shop ID không hợp lệ" });
                var result = await _service.UpdateCategory(categoryDTO);
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
