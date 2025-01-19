using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Order;
using AllFoodAPI.WebApi.Models.Shop;
using Microsoft.AspNetCore.Mvc;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _service.GetAllOrders();
                return Ok(new { success = true, data = orders });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("orderId={orderId:int}")]
        public async Task<ActionResult<AddressDTO>> GetOrderById(int orderId)
        {
            if (orderId == 0) return BadRequest(new { success = false, message = "ID không hợp lệ." });
            try
            {
                var order = await _service.GetOrderById(orderId);
                if (order == null) return NotFound(new { success = false, message = "Không tìm thấy hoá đơn này" });
                return Ok(new { success = true, data = order });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }


        [HttpGet("userId={userId:int}")]
        public async Task<IActionResult> GetỎderByUserId(int userId)
        {
            if (userId == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ." });
            try
            {
                var orders = await _service.GetOrdersByUserId(userId);
                return Ok(new { success = true, data = orders });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderModel order)
        {
            if (order.Total < 0) return BadRequest(new { success = false, message = "Total không hợp lệ" });
            if (string.IsNullOrEmpty(order.DeliveryAddress)) return BadRequest(new { success = false, message = "Địa chỉ không được rỗng" });
            if (string.IsNullOrEmpty(order.FullNameUser)) return BadRequest(new { success = false, message = "Tên khách hàng không hợp lệ" });
            if (string.IsNullOrEmpty(order.ShopName)) return BadRequest(new { success = false, message = "Tên shop không được rỗng" });
            if (string.IsNullOrEmpty(order.PhoneNum) || order.PhoneNum.Length != 10) return BadRequest(new { success = false, message = "Số điện thoại không hợp lệ" });
            try
            {
                var result = await _service.AddOrder(order);
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


        [HttpDelete("remove/{orderId:int}")]
        public async Task<IActionResult> DeleteShop(int orderId)
        {
            try
            {
                if (orderId == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteOrder(orderId);
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

        [HttpPut("update/orderId={orderId:int}/userId={userId:int}/orderStatus={orderStatus:int}")]
        public async Task<IActionResult> UpdateOrder(int orderId, int userId, int orderStatus)
        {
            try
            {
                if (orderId == 0) return BadRequest(new { success = false, message = "Order ID không hợp lệ" });
                if (userId == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ" });
                if (orderStatus < 0 || orderStatus > 3) return BadRequest(new { success = false, message = "Order Status không hợp lệ" });
                
                var result = await _service.UpdateOrder(orderId, userId, orderStatus);
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
