using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Shop;
using AllFoodAPI.WebApi.Models.Voucher;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/voucher")]
    [ApiController]
    public class VoucherCotroller : ControllerBase
    {
        private readonly IVoucherService _service;
        public VoucherCotroller(IVoucherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVouchers()
        {
            try
            {
                var shops = await _service.GetAllVouchers();
                return Ok(shops);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VoucherDTO>> GetVoucherById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ." });
            try
            {
                var shop = await _service.GetVoucherById(id);
                if (shop == null) return NotFound(new { success = false, message = "Không tìm thấy voucher này" });
                return Ok(shop);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVoucher([FromBody] AddVoucherModel voucher)
        {
            if (voucher.ShopId == 0) return BadRequest(new { success = false, message = "Shop ID không hợp lệ" });
            if (string.IsNullOrEmpty(voucher.Title)) return BadRequest(new { success = false, message = "Tiêu đề voucher không được rỗng" });
            if (string.IsNullOrEmpty(voucher.Description)) return BadRequest(new { success = false, message = "Mô tả voucher không được rỗng" });
            if (voucher.Discount < 1 || voucher.Discount > 100) return BadRequest(new { success = false, message = "Discount không không hợp lệ" });
            if (voucher.StartDate < DateOnly.FromDateTime(DateTime.Now) || voucher.StartDate.Year > 9999)
                return BadRequest(new { success = false, message = "Ngày bắt đầu không hợp lệ" });
            if (voucher.EndDate < DateOnly.FromDateTime(DateTime.Now) || voucher.EndDate < voucher.StartDate || voucher.EndDate.Year > 9999)
                return BadRequest(new { success = false, message = "Ngày kết thúc không hợp lệ" });
            try
            {
                var result = await _service.AddVoucher(voucher);
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


        [HttpDelete("remove/{id:int}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteVoucher(id);
                return result
                   ? Ok(new { success = true, message = "Xóa thành công" })
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
        public async Task<IActionResult> UpdateVoucher([FromBody] UpdateVoucherModel voucher, int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "Voucher ID không hợp lệ" });
                if (string.IsNullOrEmpty(voucher.Title)) return BadRequest(new { success = false, message = "Tiêu đề voucher không được rỗng" });
                if (string.IsNullOrEmpty(voucher.Description)) return BadRequest(new { success = false, message = "Mô tả voucher không được rỗng" });
                if (voucher.Discount < 1 || voucher.Discount > 100) return BadRequest(new { success = false, message = "Discount không không hợp lệ" });
                if (voucher.StartDate < DateOnly.FromDateTime(DateTime.Now) || voucher.StartDate.Year > 9999)
                    return BadRequest(new { success = false, message = "Ngày bắt đầu không hợp lệ" });
                if (voucher.EndDate < DateOnly.FromDateTime(DateTime.Now) || voucher.EndDate < voucher.StartDate || voucher.EndDate.Year > 9999)
                    return BadRequest(new { success = false, message = "Ngày kết thúc không hợp lệ" });
                var result = await _service.UpdateVoucher(voucher, id);
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
