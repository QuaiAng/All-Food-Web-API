using AllFoodAPI.Core.DTOs;
using AllFoodAPI.WebApi.Models;

namespace AllFoodAPI.Core.Interfaces.IService
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDTO>> GetAllAddresses();
        Task<AddressDTO?> GetAddressById(int id);
        Task<bool> AddAddress(AddressDTO address);
        Task<bool> UpdateAddress(int id, string address);
        Task<bool> DeleteAddress(int id);
        Task<bool> IsAddressExist(string address, int userID);
    }
}
