using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report?> GetReportById(int id);
        Task<bool> AddReport(Report report);
        Task<bool> UpdateReport(Report report);
        Task<bool> DeleteReport(int id);
    }
}
