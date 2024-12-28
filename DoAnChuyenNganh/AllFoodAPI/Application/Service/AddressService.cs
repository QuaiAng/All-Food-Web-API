using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IService;
using AllFoodAPI.Infrastructure.Repositories;
using AllFoodAPI.WebApi.Models;
using System.Diagnostics.Metrics;

namespace AllFoodAPI.Application.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IUserRepository _userRepository;

        public AddressService(IAddressRepository repository, IUserRepository userRepository) 
        {
        
            _repository = repository;
            _userRepository = userRepository;


        }
       
        public async Task<bool> AddAddress(AddressDTO addressDTO)
        {
            try
            {
                if (await _userRepository.GetUserById(addressDTO.UserId) == null) throw new DuplicateException("User ID", "User ID không tồn tại");
                if (await IsAddressExist(addressDTO.Address.Trim(), addressDTO.UserId)) throw new DuplicateException("Address", "Địa chỉ này đã tồn tại cho user này");
                var address = AddressDTO.ToEntity(addressDTO);
                return await _repository.AddAddress(address);
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
                var address = await _repository.GetAddressById(id);
                if (address == null) throw new DuplicateException("Address", "Không tìm thấy địa chỉ");
                return await _repository.DeleteAddress(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
                
        }

        public async Task<AddressDTO?> GetAddressById(int id)
        {
            try
            {
                var address = await _repository.GetAddressById(id);
                if (address == null)
                { 
                    return null;
                }
                var addressDTO = AddressDTO.FromEntity(address);
                return addressDTO;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<IEnumerable<AddressDTO>> GetAllAddresses()
        {
            
            try
            {
                var addresses = await _repository.GetAllAddresses();
                var addressDTOs = addresses.Select(u => AddressDTO.FromEntity(u));

                return addressDTOs;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);

            }
        }

        public async Task<bool> IsAddressExist(string address, int userID)
        {
            if (string.IsNullOrEmpty(address)) return true;
            if (userID == 0) return true;
            return await _repository.IsAddressExist(address, userID);
        }

        public Task<bool> UpdateAddress(int id, string address)
        {
            throw new NotImplementedException();
        }
    }
}
