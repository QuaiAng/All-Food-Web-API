using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Report;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDTO>> GetAllReports();
        Task<ReportDTO?> GetReportById(int id);
        Task<bool> AddReport(AddReportModel report);
        Task<bool> UpdateReport(string reason, int id);
        Task<bool> DeleteReport(int id);
    }
}
