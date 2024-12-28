﻿using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Metrics;

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
                return Ok(addresses);
            }
            catch (Exception ex)
            {


                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });

            }

        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AddressDTO>> GetAddressById(int id)
        {
            if (id == 0) return BadRequest(new {success = false, message = "ID không hợp lệ." });
            try
            {
                var address = await _service.GetAddressById(id);
                if(address == null) return NotFound(new {success = false, message = "Không tìm thấy địa chỉ này" });
                return Ok(address);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Đã xảy ra lỗi" });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAddress([FromBody]AddressDTO address)
        {
            if (address.UserId == 0) return BadRequest(new { success = false, message = "ID user không hợp lệ" });
            if (string.IsNullOrEmpty(address.Address)) return BadRequest(new { success = false, message = "Địa chỉ không được rỗng" });
            try
            {
                var result = await _service.AddAddress(address);
                return Ok(new { success = true, message = "Thêm thành công" });
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
                return Ok(new { success = true, messenger = "Xóa thành công"});
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
