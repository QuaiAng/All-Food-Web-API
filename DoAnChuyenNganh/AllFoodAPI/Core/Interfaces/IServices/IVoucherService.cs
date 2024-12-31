﻿using AllFoodAPI.Core.DTOs;
using AllFoodAPI.WebApi.Models.User;
using AllFoodAPI.WebApi.Models.Voucher;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface IVoucherService
    {
        Task<IEnumerable<VoucherDTO>> GetAllVouchers();
        Task<VoucherDTO?> GetVoucherById(int id);
        Task<bool> AddVoucher(AddVoucherModel voucher);
        Task<bool> UpdateVoucher(UpdateVoucherModel voucher, int id);
        Task<bool> DeleteVoucher(int id);
    }
}
