using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddressService _service;

        public AddressController(IAddressService service)
        {

            _service = service;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                var addresses = await _service.GetAllAddresses();
                return Ok(new { success = true, data = addresses });
            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });

            }

        }
        [HttpGet("addressId={id:int}")]
        public async Task<ActionResult<AddressDTO>> GetAddressById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ." });
            try
            {
                var address = await _service.GetAddressById(id);
                if (address == null) return NotFound(new { success = false, message = "Không tìm thấy địa chỉ này" });
                return Ok(new { success = true, data = address });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpGet("userId={id:int}")]
        public async Task<ActionResult<AddressDTO>> GetAddressByUserId(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ." });
            try
            {
                var address = await _service.GetAddressByUserId(id);
                if (address == null) return NotFound(new { success = false, message = "Không tìm thấy địa chỉ" });
                return Ok(new { success = true, data = address });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAddress([FromBody] AddressDTO address)
        {
            if (address.UserId == 0) return BadRequest(new { success = false, message = "ID user không hợp lệ" });
            if (string.IsNullOrEmpty(address.Address)) return BadRequest(new { success = false, message = "Địa chỉ không được rỗng" });
            try
            {
                var result = await _service.AddAddress(address);
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


        [HttpDelete("remove/{id:int}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteAddress(id);
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
        public async Task<IActionResult> UpdateAddress([FromBody] string address, int id)
        {
            try
            {
                if (string.IsNullOrEmpty(address)) return BadRequest(new { success = false, message = "Địa chỉ rỗng" });
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.UpdateAddress(id, address);
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
