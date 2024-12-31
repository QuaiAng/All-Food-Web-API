using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AllfoodDbContext _context;

        public ReportRepository(AllfoodDbContext context)
        {
            _context = context;    
        }
        public async Task<bool> AddReport(Report report)
        {
            try
            {
                _context.Reports.Add(report);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<bool> DeleteReport(int id)
        {
            try
            {
                var report = await _context.Reports.SingleOrDefaultAsync(u => u.ReportId == id);
                if (report == null) return false;
                report.Status = false;
                _context.Reports.Update(report);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            try
            {
                var reports = await _context.Reports.Where(u => u.Status == true).ToListAsync();
                return reports;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Report?> GetReportById(int id)
        {
            try
            {
                var report = await _context.Reports.SingleOrDefaultAsync(u => u.ReportId == id && u.Status == true);
                if (report == null)
                    return null;
                return report;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateReport(Report report)
        {
            try
            {
                var reportUpdate = await _context.Reports.SingleOrDefaultAsync(u => u.ReportId == report.ReportId && u.Status == true);
                if (reportUpdate == null)
                {
                    return false;
                }
                _context.Reports.Update(reportUpdate);
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
