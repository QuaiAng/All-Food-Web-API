using AllFoodAPI.Core.Interfaces.IServices;
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
                var image = await _service.GetCartByUserId(userId);
                if (image == null) return NotFound(new { success = false, message = "Không tìm thấy giỏ hàng" });
                return Ok(image);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
    }
}
