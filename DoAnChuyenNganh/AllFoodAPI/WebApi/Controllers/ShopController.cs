using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using AllFoodAPI.WebApi.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _service;

        public ShopController(IShopService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllShops()
        {
            try
            {
                var shops = await _service.GetAllShops();
                return Ok(new { success = true, data = shops });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("shopId={shopId:int}")]
        public async Task<ActionResult<AddressDTO>> GetShopById(int shopId)
        {
            if (shopId == 0) return BadRequest(new { success = false, message = "ID không hợp lệ." });
            try
            {
                var shop = await _service.GetShopById(shopId);
                if (shop == null) return NotFound(new { success = false, message = "Không tìm thấy shop này" });
                return Ok(new { success = true, data = shop });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


        [HttpGet("userId={userId:int}")]
        public async Task<ActionResult<AddressDTO>> GetShopByUserId(int userId)
        {
            if (userId == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ." });
            try
            {
                var shop = await _service.GetShopByUserId(userId);
                if (shop == null) return NotFound(new { success = false, message = "Không tìm thấy shop này" });
                return Ok(new { success = true, data = shop });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShop([FromBody] AddShopModel shop)
        {
            if (shop.UserId == 0) return BadRequest(new { success = false, message = "ID user không hợp lệ" });
            if (string.IsNullOrEmpty(shop.Address)) return BadRequest(new { success = false, message = "Địa chỉ không được rỗng" });
            if (string.IsNullOrEmpty(shop.Phone) || shop.Phone.Length != 10) return BadRequest(new { success = false, message = "Số điện thoại không hợp lệ" });
            if (string.IsNullOrEmpty(shop.ShopName)) return BadRequest(new { success = false, message = "Tên shop không được rỗng" });
            try
            {
                var result = await _service.AddShop(shop);
                return result
                    ? Ok(new { success = true, message = "Thêm thành công" })
                    : StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        message = "Thêm thất bại"
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }


        [HttpDelete("remove/{shopId:int}")]
        public async Task<IActionResult> DeleteShop(int shopId)
        {
            try
            {
                if (shopId == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteShop(shopId);
                return Ok(new { success = true, messenger = "Xóa thành công" });
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
                throw new Exception(ex.Message);
            }

        }
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateShop([FromBody] UpdateShopModel shop, int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID shop không hợp lệ" });
                if (string.IsNullOrEmpty(shop.Address)) return BadRequest(new { success = false, message = "Địa chỉ không được rỗng" });
                if (string.IsNullOrEmpty(shop.Phone) || shop.Phone.Length != 10) return BadRequest(new { success = false, message = "Số điện thoại không hợp lệ" });
                if (string.IsNullOrEmpty(shop.ShopName)) return BadRequest(new { success = false, message = "Tên shop không được rỗng" });
                var result = await _service.UpdateShop(shop, id);
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
