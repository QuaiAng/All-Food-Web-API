using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AllfoodDbContext _context;

        public ReviewRepository(AllfoodDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddReview(Review review)
        {
            try
            {
                _context.Reviews.Add(review);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);
            }
        }

        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                var review = await _context.Reviews.SingleOrDefaultAsync(u => u.ReviewId == id);
                if (review == null) return false;
                review.Status = false;
                _context.Reviews.Update(review);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
            try
            {
                var reviews = await _context.Reviews.Where(u => u.Status == true).ToListAsync();
                return reviews;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Review?> GetReviewById(int id)
        {
            try
            {
                var review = await _context.Reviews.SingleOrDefaultAsync(u => u.ReviewId == id && u.Status == true);
                if (review == null)
                    return null;
                return review;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateReview(Review review)
        {
            try
            {
                var reviewUpdate = await _context.Reviews.SingleOrDefaultAsync(u => u.ReviewId == review.ReviewId && u.Status == true);
                if (reviewUpdate == null)
                {
                    return false;
                }
                _context.Reviews.Update(reviewUpdate);
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
