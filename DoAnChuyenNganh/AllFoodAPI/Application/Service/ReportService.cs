using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.Infrastructure.Repositories;
using AllFoodAPI.WebApi.Models.Report;

namespace AllFoodAPI.Application.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ReportService(IReportRepository reportRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _reportRepository = reportRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<bool> AddReport(AddReportModel report)
        {
            try
            {
                if (await _productRepository.GetProductById(report.ProductId) == null)
                    throw new DuplicateException("ProductID", $"Sản phẩm có ID {report.ProductId} không tồn tại");
                if (await _userRepository.GetUserById(report.UserId) == null)
                    throw new DuplicateException("UserID", $"User có ID {report.UserId} không tồn tại");

                var reportAdd = new Report
                {
                    ProductId = report.ProductId,
                    UserId = report.UserId,
                    Reason = report.Reason,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = true
                };

                return await _reportRepository.AddReport(reportAdd);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteReport(int id)
        {
            try
            {
                if (await _reportRepository.GetReportById(id) == null)
                    throw new DuplicateException("ID", $"Không tồn tại review có ID {id}");
                return await _reportRepository.DeleteReport(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ReportDTO>> GetAllReports()
        {
            try
            {
                var reports = await _reportRepository.GetAllReports();
                var reportDTOs = reports.Select(u => ReportDTO.FromEntity(u));
                return reportDTOs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ReportDTO?> GetReportById(int id)
        {
            try
            {
                var report = await _reportRepository.GetReportById(id);
                if (report == null) return null;
                return ReportDTO.FromEntity(report);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateReport(string reason, int id)
        {
            try
            {
                var reportUpdate = await _reportRepository.GetReportById(id);
                if (reportUpdate == null)
                    throw new DuplicateException("ReviewID", $"Không tồn tại review có ID {id}");
                reportUpdate.Reason = reason;
                return await _reportRepository.UpdateReport(reportUpdate);
            }
            catch
            {
                throw;
            }
        }
    }
}
