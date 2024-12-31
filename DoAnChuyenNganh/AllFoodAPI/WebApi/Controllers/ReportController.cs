using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Report;
using AllFoodAPI.WebApi.Models.Review;
using Microsoft.AspNetCore.Mvc;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service) 
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                var reports = await _service.GetAllReports();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReportDTO>> GetReportById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ." });
            try
            {
                var report = await _service.GetReportById(id);
                if (report == null) return NotFound(new { success = false, message = "Không tìm thấy report này" });
                return Ok(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReport([FromBody] AddReportModel report)
        {
            if (report.ProductId == 0) return BadRequest(new { success = false, message = "Product ID không hợp lệ" });
            if (report.UserId == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ" });
            if (string.IsNullOrEmpty(report.Reason)) return BadRequest(new { success = false, message = "Lí do không được rỗng" });

            try
            {
                var result = await _service.AddReport(report);
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
        public async Task<IActionResult> DeleteReport(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteReport(id);
                return result
                   ? Ok(new { success = true, message = "Xóa thành công" })
                   : StatusCode(StatusCodes.Status500InternalServerError, new
                   {
                       success = false,
                       message = "Xóa thất bại"
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
        public async Task<IActionResult> UpdateReport([FromBody] string reason, int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "Report ID không hợp lệ" });
                if (string.IsNullOrEmpty(reason)) return BadRequest(new { success = false, message = "Lí do không được rỗng" });

                var result = await _service.UpdateReport(reason, id);
                return result ? Ok(new
                {
                    success = true,
                    message = "Cập nhật thành công"
                }) : StatusCode(StatusCodes.Status500InternalServerError, new
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
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

    }
}
