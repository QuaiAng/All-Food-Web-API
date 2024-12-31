using AllFoodAPI.Core.DTOs;
using AllFoodAPI.WebApi.Models.Review;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetAllReviews();
        Task<ReviewDTO?> GetReviewById(int id);
        Task<bool> AddReview(AddReviewModel review);
        Task<bool> UpdateReview(UpdateReviewModel review, int id);
        Task<bool> DeleteReview(int id);
    }
}
