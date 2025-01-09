using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.User;

namespace AllFoodAPI.Core.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User> Login(string username, string password);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> IsUserNameExist(string username);
        Task<bool> IsEmailExist(string username);
        Task<bool> IsPhoneExist(string username);
        Task<bool> ChangePassword(User changePassword);

    }
}
