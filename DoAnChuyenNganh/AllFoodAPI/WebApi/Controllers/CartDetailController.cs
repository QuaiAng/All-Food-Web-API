using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Cart;
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
        public async Task<IActionResult> AddCartDetail([FromBody]AddCartDetailModel cartDetailDTO)
        {
            if (cartDetailDTO.ProductId == 0) return BadRequest(new { success = false, message = "ProductID không hợp lệ" });
            if (cartDetailDTO.Quantity < 1) return BadRequest(new { success = false, message = "Số lượng không hợp lệ" });
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

        [HttpDelete("remove/productId={productId:int}/cartId={cartId:int}")]
        public async Task<IActionResult> DeleteCartDetail(int productId, int cartId)
        {
            if (productId == 0) return BadRequest(new { success = false, message = "ProductID không hợp lệ" });
            if (cartId == 0) return BadRequest(new { success = false, message = "CartID không hợp lệ" });

            try
            {
                var result = await _service.DeleteCartDetail(productId, cartId);
                return result
                    ? Ok(new { success = true, message = "Xoá thành công" })
                    : StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        message = "Xoá thất bại"
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
        
        [HttpPut("updatequantity={quantity:int}/productId={productId:int}/cartId={cartId:int}")]
        public async Task<IActionResult> UpdateCartDetail(int quantity, int productId, int cartId)
        {
            if (productId == 0) return BadRequest(new { success = false, message = "ProductID không hợp lệ" });
            if (quantity == 0) return BadRequest(new { success = false, message = "Số lượng không hợp lệ" });
            if (cartId == 0) return BadRequest(new { success = false, message = "CartID không hợp lệ" });
           
            try
            {
                var result = await _service.UpdateCartDetail(quantity, productId, cartId);
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
