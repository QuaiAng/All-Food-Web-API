using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.Infrastructure.Repositories;
using AllFoodAPI.WebApi.Models.Review;
using NuGet.Protocol.Core.Types;
using System.Diagnostics.Metrics;

namespace AllFoodAPI.Application.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public ReviewService(IReviewRepository reviewRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }
        public async Task<bool> AddReview(AddReviewModel review)
        {
            try
            {
                if (await _productRepository.GetProductById(review.ProductId) == null)
                    throw new DuplicateException("ProductID", $"Sản phẩm có ID {review.ProductId} không tồn tại");
                if (await _userRepository.GetUserById(review.UserId) == null)
                    throw new DuplicateException("UserID", $"User có ID {review.UserId} không tồn tại");

                var reviewAdd = new Review
                {
                    ProductId = review.ProductId,
                    UserId = review.UserId,
                    Comment = review.Comment,
                    Rating = review.Rating,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Status = true
                };

                return await _reviewRepository.AddReview(reviewAdd);
            }
            catch { throw; }
        }

        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                if (await _reviewRepository.GetReviewById(id) == null)
                    throw new DuplicateException("ID", $"Không tồn tại review có ID {id}");
                return await _reviewRepository.DeleteReview(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewRepository.GetAllReviews();
                var reviewDTOs = reviews.Select(u => ReviewDTO.FromEntity(u));
                return reviewDTOs;
            }
            catch
            {
                throw;
            }
        }
    

    public async Task<ReviewDTO?> GetReviewById(int id)
    {
        try
        {
            var review = await _reviewRepository.GetReviewById(id);
            if (review == null) return null;
            return ReviewDTO.FromEntity(review);
        }
        catch
        {
            throw;
        }
    }

        public async Task<IEnumerable<ReviewDTO>> GetReviewByProductId(int productId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewByProductId(productId);
                
                var reviewDTOs = reviews.Select(u => ReviewDTO.FromEntity(u));
                return reviewDTOs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateReview(UpdateReviewModel review, int id)
        {
            try
            {
                var reviewUpdate = await _reviewRepository.GetReviewById(id);
                if (reviewUpdate == null)
                    throw new DuplicateException("ReviewID", $"Không tồn tại review có ID {id}");
                reviewUpdate.Comment = review.Comment;
                reviewUpdate.Rating = review.Rating;
                return await _reviewRepository.UpdateReview(reviewUpdate);
            }
            catch
            {
                throw;
            }
        }
    }
}
