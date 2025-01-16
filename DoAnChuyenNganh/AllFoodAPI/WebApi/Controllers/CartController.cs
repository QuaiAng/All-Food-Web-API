using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }


        [HttpGet("userId={userId:int}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            if (userId == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ" });
            try
            {
                var cart = await _service.GetCartByUserId(userId);
                if (cart == null) return NotFound(new { success = false, message = "Không tìm thấy giỏ hàng" });
                return Ok(new { success = true, data = cart });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddCart([FromBody] AddCartModel cart)
        {
            if (cart.UserId == 0) return BadRequest(new { success = false, message = "ID user không hợp lệ" });
            if (cart.Total < 0) return BadRequest(new { success = false, message = "Total không hợp lệ" });
            try
            {
                var result = await _service.AddCart(cart);
                return Ok(new { success = true, message = "Thêm thành công" });
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

    }
}
