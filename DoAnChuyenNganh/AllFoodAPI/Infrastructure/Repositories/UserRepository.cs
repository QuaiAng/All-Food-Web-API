using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Infrastructure.Data;
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
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id && u.Status == true);
                if (user == null) return false;
                user.Status = false;
                _context.Users.Update(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting user: {ex.Message}");
                throw;

            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                              .Where(u => u.Status == true)
                              .Include(u => u.Addresses)
                              .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<User?> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id && u.Status == true);

                if (user == null)
                {
                    return null;
                }


                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Ném lại exception
                throw;
            }
        }


        //Nếu tồn tại user có username và password trùng khớp thì trả về true, ngược lại trả về false
        public async Task<User?> Login(string username, string password)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    return null;
                }

                string passwordHashed = PasswordHasher.HashPassword(password, user.Salt);

                if (user.Password == passwordHashed)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Ném lại exception
                throw;
            }
        }


        public async Task<bool> UpdateUser(User userUpdate)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userUpdate.UserId);
                if (user == null)
                {
                    return false;
                }

                _context.Users.Update(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

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
