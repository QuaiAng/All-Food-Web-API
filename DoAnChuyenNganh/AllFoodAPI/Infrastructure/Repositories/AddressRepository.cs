using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AllfoodDbContext _context;

        public AddressRepository(AllfoodDbContext context)
        {

            _context = context;

        }
        public async Task<bool> AddAddress(Address address)
        {
            try
            {
                _context.Addresses.Add(address);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<bool> DeleteAddress(int id)
        {
            try
            {
                var address = await _context.Addresses.SingleOrDefaultAsync(u => u.AddressId == id);
                if (address == null) return false;
                _context.Addresses.Remove(address);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<Address?> GetAddressById(int id)
        {
            try
            {
                var adddress = await _context.Addresses.SingleOrDefaultAsync(u => u.AddressId == id);

                if (adddress == null)
                {
                    return null;
                }

                return adddress;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            try
            {
                return await _context.Addresses.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> IsAddressExist(string address, int userID)
        {
            return await _context.Addresses.SingleOrDefaultAsync(u => u.Address1 == address && u.AddressId == userID) != null;
        }

        public async Task<bool> UpdateAddress(Address addressUpdate)
        {
            try
            {
                var address = await _context.Addresses.SingleOrDefaultAsync(u => u.AddressId == addressUpdate.AddressId);
                if (address == null)
                {
                    return false;
                }
                _context.Addresses.Update(address);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
