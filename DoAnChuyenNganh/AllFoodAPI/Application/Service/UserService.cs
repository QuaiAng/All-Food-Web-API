using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces;
using AllFoodAPI.Shared.Helpers;
using AllFoodAPI.WebApi.Models;
using System.Diagnostics.Metrics;

namespace AllFoodAPI.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }




        //Hàm thêm mới 1 user
        public async Task<bool> AddUser(AddUserModel user)
        {

            try
            {
                if (await _userRepository.IsEmailExist(user.Email)) throw new DuplicateException("Email", "Email đã tồn tại");
                if (await _userRepository.IsUserNameExist(user.Username)) throw new DuplicateException("Username", "Username đã tồn tại");
                if (await _userRepository.IsPhoneExist(user.Phone)) throw new DuplicateException("Phone", "Số điện thoại đã tồn tại");

                var salt = PasswordHasher.GenerateSalt();   //Sinh ra Salt ngẫu nhiên
                var hashedPassword = PasswordHasher.HashPassword(user.Password, salt);   //Băm mật khẩu

                //Chuyển từ userDTO sang User Entity với mật khẩu đã được băm
                var userEntity = new User
                {
                    UserId = 0,
                    FullName = user.FullName,
                    Email = user.Email,
                    Username = user.Username,
                    Password = hashedPassword,
                    Phone = user.Phone,
                    ImageUrl = "",
                    Salt = salt,
                    Status = true,

                };

                return await _userRepository.AddUser(userEntity);
            }
            catch (Exception ex) { throw; }

        }


        public Task<bool> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public Task<bool> IsEmailExist(string email)
        {
            return _userRepository.IsEmailExist(email);
        }

        public Task<bool> IsPhoneExist(string phone)
        {
            return _userRepository.IsPhoneExist(phone);

        }

        public Task<bool> IsUserNameExist(string username)
        {
            return _userRepository.IsUserNameExist(username);

        }

        public async Task<string?> Login(string username, string password)
        {

            // Kiểm tra người dùng trong repository
            bool user = await _userRepository.Login(username, password);

            if (user == false)
            {
                return string.Empty;
            }
            string Token = _jwtService.GenerateToken(username);
            return Token;
        }

        public async Task<bool> UpdateUser(UpdateUserModel userUpdate, int id)
        {
            try
            {
                var userTemp = await _userRepository.GetUserById(id);
                if (userTemp == null)
                    throw new DuplicateException("User", "User not found");

                if (userTemp.Email != userUpdate.Email)
                {
                    bool rs = await IsEmailExist(userUpdate.Email);
                    if (rs)
                        throw new DuplicateException("Email", "Email đã tồn tại");
                }

                if (userTemp.Phone != userUpdate.Phone)
                {
                    bool rs = await IsPhoneExist(userUpdate.Phone);
                    if (rs)
                        throw new DuplicateException("Phone", "Số điện thoại đã tồn tại");
                }

                var hashedPassword = PasswordHasher.HashPassword(userUpdate.Password, userTemp.Salt);
                userUpdate.Password = hashedPassword;
                return await _userRepository.UpdateUser(userUpdate, id);
            }
            catch (Exception ex)
            {
                throw; // Throw lại exception để Controller xử lý
            }
        }
    }
}
