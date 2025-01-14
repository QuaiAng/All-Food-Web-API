using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string? FullName { get; set; }

        public string? Comment { get; set; }

        public double Rating { get; set; }

        public DateOnly Date { get; set; }


        public static ReviewDTO FromEntity(Review review) => new ReviewDTO
        {
            ReviewId = review.ReviewId,
            ProductId = review.ProductId,
            UserId = review.UserId,
            Comment = review.Comment,
            Rating = review.Rating,
            Date = review.Date,
            FullName = review.User.FullName,
        };

        public static Review ToEntity(ReviewDTO review) => new Review
        {
            ReviewId = review.ReviewId,
            ProductId = review.ProductId,
            UserId = review.UserId,
            Comment = review.Comment,
            Rating = review.Rating,
            Date = review.Date,
        };
    }
}
