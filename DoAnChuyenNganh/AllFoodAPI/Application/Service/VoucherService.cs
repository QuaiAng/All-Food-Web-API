using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Voucher;

namespace AllFoodAPI.Application.Service
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _repository;
        private readonly IShopRepository _shopRepository;

        public VoucherService(IVoucherRepository repository, IShopRepository shopRepository)
        {
            _repository = repository;
            _shopRepository = shopRepository;
        }
        public async Task<bool> AddVoucher(AddVoucherModel voucher)
        {
            try
            {
                if (await _shopRepository.GetShopById(voucher.ShopId) == null) 
                    throw new DuplicateException("Shop ID", $"Shop có ID {voucher.ShopId} kHông tồn tại");
                var voucherAdd = new Voucher
                {
                    ShopId = voucher.ShopId,
                    Title = voucher.Title,
                    Description = voucher.Description,
                    Discount = voucher.Discount,
                    StartDate = voucher.StartDate,
                    EndDate = voucher.EndDate,
                    Quantity = voucher.Quantity,
                    PaymentMethod = voucher.PaymentMethod,
                    Status = true

                };
                return await _repository.AddVoucher(voucherAdd);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            try
            {
                if (await _repository.GetVoucherById(id) == null)
                    throw new DuplicateException("ID", $"Không tồn tại voucher có ID {id}");
                return await _repository.DeleteVoucher(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<VoucherDTO>> GetAllVouchers()
        {
            try
            {
                var vouchers = await _repository.GetAllVouchers();
                var voucherDTOs = vouchers.Select(u => VoucherDTO.FromEntity(u));
                return voucherDTOs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<VoucherDTO?> GetVoucherById(int id)
        {
            try
            {
                var voucher = await _repository.GetVoucherById(id);
                if (voucher == null) return null;
                return VoucherDTO.FromEntity(voucher);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateVoucher(UpdateVoucherModel voucher, int id)
        {
            try
            {
                var voucherUpdate = await _repository.GetVoucherById(id);
                if (voucherUpdate == null) 
                    throw new DuplicateException("CategoryID", $"Không tồn tại ID {id}");
                voucherUpdate.StartDate = voucher.StartDate;
                voucherUpdate.EndDate = voucher.EndDate;
                voucherUpdate.Description = voucher.Description;
                voucherUpdate.Quantity = voucher.Quantity;
                voucherUpdate.Title = voucher.Title;
                voucherUpdate.Discount = voucher.Discount;
                voucherUpdate.PaymentMethod = voucher.PaymentMethod;
                return await _repository.UpdateVoucher(voucherUpdate);
            }
            catch
            {
                throw;
            }
        }
    }
}
