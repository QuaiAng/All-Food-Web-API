using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class ReportDTO
    {
        public int ReportId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string Reason { get; set; } = null!;

        public DateOnly Date { get; set; }


        public static ReportDTO FromEntity(Report report) => new ReportDTO
        {
            ReportId = report.ReportId,
            ProductId = report.ProductId,
            UserId = report.UserId,
            Reason = report.Reason,
            Date = report.Date,
        };
        
        public static Report ToEntity(ReportDTO report) => new Report
        {
            ReportId = report.ReportId,
            ProductId = report.ProductId,
            UserId = report.UserId,
            Reason = report.Reason,
            Date = report.Date,
        };
    }
}
