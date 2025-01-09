using AllFoodAPI.Core.DTOs;
using AllFoodAPI.WebApi.Models.User;

namespace AllFoodAPI.Core.Interfaces.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO?> GetUserById(int id);
        Task<ResponseLoginModel?> Login(string username, string password);

        Task<bool> AddUser(AddUserModel user);
        Task<bool> UpdateUser(UpdateUserModel user, int id);
        Task<bool> DeleteUser(int id);
        Task<bool> IsUserNameExist(string username);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneExist(string phone);
        Task<bool> ChangePassword(ChangePasswordModel changePassword, int id);
    }
}
