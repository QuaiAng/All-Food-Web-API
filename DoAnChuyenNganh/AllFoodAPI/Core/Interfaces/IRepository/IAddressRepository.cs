using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepository
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAddresses();
        Task<Address?> GetAddressById(int id);
        Task<bool> AddAddress(Address address);
        Task<bool> UpdateAddress(int id, string address);
        Task<bool> DeleteAddress(int id);
        Task<bool> IsAddressExist(string address, int userID);
    }
}
