﻿using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IService;
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
                    FullName = user.FullName.Trim(),
                    Email = user.Email.Trim(),
                    Username = user.Username,
                    Password = hashedPassword,
                    Phone = user.Phone.Trim(),
                    ImageUrl = "",
                    Salt = salt,
                    Status = true,

                };

                return await _userRepository.AddUser(userEntity);
            }
            catch { throw; }

        }

        public async Task<bool> DeleteUser(int id)
        {
            
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user == null) throw new DuplicateException("User", "Không tìm thấy user");
                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");

                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();

                var userDTOs = users.Select(user => UserDTO.FromEntity(user));

                return userDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all users: {ex.Message}");

                throw new Exception("Có lỗi xảy ra khi lấy danh sách người dùng.");
            }   
        }


        public async Task<UserDTO?> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                {
                    return null;
                }

                var userDTO = UserDTO.FromEntity(user);

                return userDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserById: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsEmailExist(string email)
        {
            try
            {
                return await _userRepository.IsEmailExist(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsEmailExist: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsPhoneExist(string phone)
        {
            try
            {
                return await _userRepository.IsPhoneExist(phone);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsPhoneExist: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> IsUserNameExist(string username)
        {
            try
            {
                return await _userRepository.IsUserNameExist(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsUserNameExist: {ex.Message}");
                throw;
            }
        }

        public async Task<string?> Login(string username, string password)
        {
            try
            {
                // Kiểm tra người dùng trong repository
                bool user = await _userRepository.Login(username, password);

                if (!user)
                {
                    return string.Empty;
                }

                string token = _jwtService.GenerateToken(username);
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Login: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateUser(UpdateUserModel userUpdate, int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);
                if (user == null)
                    throw new DuplicateException("User", "User not found");

                if (user.Email != userUpdate.Email)
                {
                    bool rs = await IsEmailExist(userUpdate.Email);
                    if (rs)
                        throw new DuplicateException("Email", "Email đã tồn tại");
                }

                if (user.Phone != userUpdate.Phone)
                {
                    bool rs = await IsPhoneExist(userUpdate.Phone);
                    if (rs)
                        throw new DuplicateException("Phone", "Số điện thoại đã tồn tại");
                }

                var hashedPassword = PasswordHasher.HashPassword(userUpdate.Password, user.Salt);
                userUpdate.Password = hashedPassword;

                if (user != null)
                {
                    user.Email = userUpdate.Email;
                    user.FullName = userUpdate.FullName;
                    user.ImageUrl = userUpdate.ImageUrl;
                    user.Password = userUpdate.Password;
                    user.Phone = userUpdate.Phone;
                }


                return await _userRepository.UpdateUser(user!);
            }
            catch
            {
                throw; // Throw lại exception để Controller xử lý
            }
        }
    }
}
