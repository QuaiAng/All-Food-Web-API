using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IService;
using AllFoodAPI.Shared.Helpers;
using AllFoodAPI.WebApi.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace AllFoodAPI.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {

            _service = service;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _service.GetAllUsers();
                return Ok(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllUsers: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Có lỗi xảy ra khi lấy danh sách người dùng." });
            }
        }


        [Route("{id:int}")]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            if (id == 0) return BadRequest(new { success = false, message = "User ID không hợp lệ" });
            try
            {
                var user = await _service.GetUserById(id);
                if (user == null)
                {

                    return NotFound(new { success = false, message = "Không tìm thấy user" });

                }
                return Ok(new { success = true, data = user });
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Có lỗi xảy ra" });
            }
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginDTO)
        {
            try
            {
                var response = await _service.Login(loginDTO.username, loginDTO.password);
                if (response == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy user" });
                }
                return Ok(new { UserId = response.UserId, Token = response.Token });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in Login: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, message = "Có lỗi xảy ra trong quá trình xử lý yêu cầu." });
            }
        }



        //Hàm thêm mới 1 user
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel user)
        {

            if (string.IsNullOrEmpty(user.Username)
                   || string.IsNullOrEmpty(user.FullName)
                   || string.IsNullOrEmpty(user.Email)
                   || string.IsNullOrEmpty(user.Password)
                   || string.IsNullOrEmpty(user.Phone))
            {

                return BadRequest(new { success = false, message = "Không được để trống các thông tin cần thiết" });

            }

            if (user.Phone.Length != 10) return BadRequest(new { success = false, message = "Số điện thoại không hợp lệ" });

            if (!DataValidator.IsValidEmail(user.Email)) return BadRequest(new { success = false, message = "Email không hợp lệ" });

            try
            {
                var result = await _service.AddUser(user);

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

                return StatusCode(500, new { success = false, message = ex.Message });

            }

        }

        //Hàm xóa 1 user khỏi CSDL
        [Route("remove/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (id == 0) return BadRequest(new { success = false, message = "ID không hợp lệ" });
                var result = await _service.DeleteUser(id);
                return StatusCode(StatusCodes.Status200OK, new { success = true, message = "Xóa thành công" });
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }

        //Hàm sửa 1 user 
        [Route("update/{id:int}")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel userUpdate, int id)
        {

            if (id == 0) return BadRequest(new { Message = "ID không hợp lệ" });

            if (string.IsNullOrEmpty(userUpdate.FullName)
                  || string.IsNullOrEmpty(userUpdate.Email)
                  || string.IsNullOrEmpty(userUpdate.Password)
                  || string.IsNullOrEmpty(userUpdate.Phone))
            {

                return BadRequest(new { Message = "Không được để trống các thông tin cần thiết" });

            }

            if (userUpdate.Phone.Length != 10) return BadRequest(new { Message = "Số điện thoại không hợp lệ" });

            if (!DataValidator.IsValidEmail(userUpdate.Email)) return BadRequest(new { Message = "Email không hợp lệ" });


            try
            {
                var result = await _service.UpdateUser(userUpdate, id);
                return Ok(new { success = true, message = "Cập nhật thành công" });
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
        }
    }
}



