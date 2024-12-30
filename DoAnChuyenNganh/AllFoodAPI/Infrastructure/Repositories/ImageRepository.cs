using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AllfoodDbContext _context;

        public ImageRepository(AllfoodDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddImage(Core.Entities.Image image)
        {
            try
            {
                _context.Images.Add(image);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);

            }
        }

        public async Task<bool> DeleteImage(int id)
        {
            try
            {
                var image = await _context.Images.SingleOrDefaultAsync(u => u.ImageId == id);
                if (image == null) return false;
                _context.Images.Remove(image);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Xảy ra lỗi khi xóa", ex);
            }
        }

        public async Task<IEnumerable<Core.Entities.Image>> GetAllImages()
        {
            try
            {
                return await _context.Images.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public Task<Core.Entities.Image?> GetImageById(int id)
        {
            try
            {
                var image = _context.Images.SingleOrDefaultAsync(u => u.ImageId == id);
                if (image == null) return null;
                return image;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> UpdateImage(Core.Entities.Image image)
        {
            try
            {
                var imageUpdate = await _context.Images.SingleOrDefaultAsync(u => u.ImageId == image.ImageId);
                if (imageUpdate == null) return false;
                imageUpdate.ImageUrl = image.ImageUrl;
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex) 
            {

                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }
    }
}
