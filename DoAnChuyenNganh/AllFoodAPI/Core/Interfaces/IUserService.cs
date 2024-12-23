﻿using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models;

namespace AllFoodAPI.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO?> GetUserById(int id);
        Task<string?> Login(string username, string password);

        Task<bool> AddUser(AddUserModel user);
        Task<bool> UpdateUser(UpdateUserModel user, int id);
        Task<bool> DeleteUser(int id);
        Task<bool> IsUserNameExist(string username);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneExist(string phone);
    }
}
