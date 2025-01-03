using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/cartdetail")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly ICartDetailService _service;

        public CartDetailController(ICartDetailService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCartDetail(CartDetailDTO cartDetailDTO)
        {
            if (cartDetailDTO.ProductId == 0) return BadRequest(new { success = false, message = "ProductID không hợp lệ" });
            if (cartDetailDTO.Quantity <= 0) return BadRequest(new { success = false, message = "Số lượng không hợp lệ" });
            if (cartDetailDTO.Price < 0 || cartDetailDTO.Price > 2000000000) return BadRequest(new { success = false, message = "Giá không hợp lệ" });
            if (cartDetailDTO.Total != cartDetailDTO.Price * cartDetailDTO.Quantity) return BadRequest(new { success = false, message = "Tổng cộng không hợp lệ" });
            try
            {
                var result = await _service.AddCartDetail(cartDetailDTO);
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
                throw new Exception(ex.Message);
            }
        }
        
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateCartDetail(int quantity, int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ProductID không hợp lệ" });
           
            try
            {
                var result = await _service.UpdateCartDetail(quantity, id);
                return result
                    ? Ok(new { success = true, message = "Cập nhật thành công" })
                    : StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        message = "Cập nhật thất bại"
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
                throw new Exception(ex.Message);
            }
        }
    }
}
