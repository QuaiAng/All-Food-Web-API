using System.ComponentModel.DataAnnotations;

namespace AllFoodAPI.WebApi.Models.Report
{
    public class AddReportModel
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }
        public string Reason { get; set; } = null!;
    }
}
