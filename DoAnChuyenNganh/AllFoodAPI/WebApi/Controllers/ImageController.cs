using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var result = await _service.GetAllImages();
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("imageId={id:int}")]
        public async Task<ActionResult<Core.Entities.Image>> GetImageById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
            try
            {
                var image = await _service.GetImageById(id);
                if (image == null) return NotFound(new { success = false, message = "Không tìm thấy hình ảnh" });
                return Ok(new { success = true, data = image });
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        
        
        [HttpGet("productId={id:int}")]
        public async Task<ActionResult<Core.Entities.Image>> GetImageByProductId(int productId)
        {
            if (productId == 0) return BadRequest(new { success = false, message = "Product ID không hợp lệ" });
            try
            {
                var image = await _service.GetImageByProductId(productId);
                if (image == null) return NotFound(new { success = false, message = "Không tìm thấy hình ảnh" });
                return Ok(new { success = true, data = image });
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddImage([FromBody]ImageDTO image)
        {
            
            try
            {
                if (image.ProductId == 0) return BadRequest(new { success = false, message = "Product ID không hợp lệ" });
                if (string.IsNullOrEmpty(image.ImageUrl)) return BadRequest(new { success = false, message = "Image URL không được rỗng" });
                var result = await _service.AddImage(image);
                return result ? Ok(new { success = true, message = "Thêm thành công" }) : StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Thêm thất bại" });
            }
            catch(DuplicateException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    field = ex.Field,
                    message = ex.Message,   
                });
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "Product ID không hợp lệ" });
                var result = await _service.DeleteImage(id);
                return result ? Ok(new { success = true, message = "Xóa thành công" }) : StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Thêm thất bại" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateImage([FromBody] string imageURL, int id)
        {
            try
            {
                if (string.IsNullOrEmpty(imageURL)) return BadRequest(new { success = false, message = "Image URL rỗng" });
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.UpdateImage(imageURL, id);
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
            catch(DuplicateException ex)
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
