using System.ComponentModel.DataAnnotations;

namespace AllFoodAPI.WebApi.Models.Review
{
    public class AddReviewModel
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string? Comment { get; set; }
        public double Rating { get; set; }

    }
}
