using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Review;
using AllFoodAPI.WebApi.Models.Voucher;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _service.GetAllReviews();
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("reviewId={id:int}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ." });
            try
            {
                var review = await _service.GetReviewById(id);
                if (review == null) return NotFound(new { success = false, message = "Không tìm thấy review này" });
                return Ok(review);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }
        
        
        [HttpGet("productId={id:int}")]
        public async Task<ActionResult<ReviewDTO>> GetReviewByProductId(int productId)
        {
            if (productId == 0) return BadRequest(new { success = false, message = "Product ID không hợp lệ." });
            try
            {
                var review = await _service.GetReviewByProductId(productId);
                if (review == null) return NotFound(new { success = false, message = "Không tìm thấy review này" });
                return Ok(review);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewModel review)
        {
            if (review.ProductId == 0) return BadRequest(new { success = false, message = "Product ID không hợp lệ" });
            if (review.UserId == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ" });
            if (review.Rating < 1 || review.Rating > 5) return BadRequest(new { success = false, message = "Rating không hợp lệ" });
           
            try
            {
                var result = await _service.AddReview(review);
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
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteReview(id);
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
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewModel review, int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "Review ID không hợp lệ" });
                if (review.Rating < 1 || review.Rating > 5) return BadRequest(new { success = false, message = "Rating không hợp lệ" });
                var result = await _service.UpdateReview(review, id);
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
