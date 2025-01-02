using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly AllfoodDbContext _context;

        public VoucherRepository(AllfoodDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddVoucher(Voucher voucher)
        {
            try
            {
                _context.Vouchers.Add(voucher);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            try
            {
                var voucher = await _context.Vouchers.SingleOrDefaultAsync(u => u.VoucherId == id && u.Status == true);
                if (voucher == null) return false;
                voucher.Status = false;
                _context.Vouchers.Update(voucher);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Voucher>> GetAllVouchers()
        {
            try
            {
                var vouchers = await _context.Vouchers.Where(u => u.Status == true).ToListAsync();
                return vouchers;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Voucher?> GetVoucherById(int id)
        {
            try
            {
                var voucher = await _context.Vouchers.SingleOrDefaultAsync(u => u.ShopId == id && u.Status == true);
                if (voucher == null)
                    return null;
                return voucher;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Voucher>> GetVoucherByShopId(int shopId)
        {
            try
            {
                var vouchers = await _context.Vouchers.Where(u => u.Status == true && u.ShopId == shopId).ToListAsync();
                return vouchers;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateVoucher(Voucher voucher)
        {
            try
            {
                var voucherUpdate = await _context.Vouchers.SingleOrDefaultAsync(u => u.VoucherId == voucher.VoucherId && u.Status == true);
                if (voucherUpdate == null)
                {
                    return false;
                }
                _context.Vouchers.Update(voucherUpdate);
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
