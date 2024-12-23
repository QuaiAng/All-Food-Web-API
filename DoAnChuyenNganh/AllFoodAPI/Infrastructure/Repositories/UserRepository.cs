using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces;
using AllFoodAPI.Infrastructure.Data;
using AllFoodAPI.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AllfoodDbContext _context;

        public UserRepository(AllfoodDbContext context)
        {

            _context = context;

        }
        public async Task<bool> AddUser(User userEntity)
        {
            
            _context.Users.Add(userEntity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
                if (user == null) return false;
                _context.Users.Remove(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {

                Console.WriteLine($"Error deleting user: {ex.Message}");
                throw;

            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                              .Where(u => u.Status == true)
                              .Select(user => UserDTO.FromEntity(user))
                              .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id && u.Status == true);

                if (user == null)
                {
                    return null;
                }
                var userDTO = UserDTO.FromEntity(user);

                return userDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Ném lại exception
                throw;
            }
        }


        //Nếu tồn tại user có username và password trùng khớp thì trả về true, ngược lại trả về false
        public async Task<bool> Login(string username, string password)
        {
            try
            {

                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    return false;
                }

                string passwordHashed = PasswordHasher.HashPassword(password, user.Salt);

                return user.Password == passwordHashed;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Ném lại exception
                throw;
            }
        }


        public async Task<bool> UpdateUser(UpdateUserModel userUpdate, int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return false;
            }

            // Cập nhật các trường của thực thể user từ userDTO
            user.Email = userUpdate.Email;
            user.FullName = userUpdate.FullName;
            user.ImageUrl = userUpdate.ImageUrl;
            user.Password = userUpdate.Password;
            user.Phone = userUpdate.Phone;
            user.Status = userUpdate.Status;

            _context.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }



      
         public async Task<bool> IsUserNameExist(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.Trim() == username.Trim());
        }

         public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.Trim() == email.Trim());
                
        }
        
        public async Task<bool> IsPhoneExist(string phone)
        {
            return await _context.Users.AnyAsync(u => u.Phone.Trim() == phone.Trim());
                
        }

    }
}
