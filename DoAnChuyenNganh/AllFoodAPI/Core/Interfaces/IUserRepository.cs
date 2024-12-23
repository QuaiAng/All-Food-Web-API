using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models;

namespace AllFoodAPI.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO?> GetUserById(int id);
        Task<bool> Login(string username, string password);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(UpdateUserModel user, int id);
        Task<bool> DeleteUser(int id);
        Task<bool> IsUserNameExist(string username);
        Task<bool> IsEmailExist(string username);
        Task<bool> IsPhoneExist(string username);
    }
}
