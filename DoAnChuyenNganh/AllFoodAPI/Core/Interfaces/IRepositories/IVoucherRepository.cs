using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Voucher;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface IVoucherRepository
    {
        Task<IEnumerable<Voucher>> GetAllVouchers();
        Task<Voucher?> GetVoucherById(int id);
        Task<bool> AddVoucher(Voucher voucher);
        Task<bool> UpdateVoucher(Voucher voucher);
        Task<bool> DeleteVoucher(int id);
    }
}
