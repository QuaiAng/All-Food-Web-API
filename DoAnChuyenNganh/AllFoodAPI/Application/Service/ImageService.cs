using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IService;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace AllFoodAPI.Application.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;

        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddImage(ImageDTO image)
        {
            try
            {
                return await _repository.AddImage(ImageDTO.ToEntity(image));
            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> DeleteImage(int id)
        {
            try
            {
                return await _repository.DeleteImage(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ImageDTO>> GetAllImages()
        {
            try
            {
                var images = await _repository.GetAllImages();
                var imageDTOs = images.Select(u => ImageDTO.FromEntity(u));
                return imageDTOs;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ImageDTO?> GetImageById(int id)
        {
            try
            {
                var imageDTO = await _repository.GetImageById(id);
                if (imageDTO == null) return null;
                return ImageDTO.FromEntity(imageDTO);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateImage(string imageURL, int id)
        {
            try
            {
                var image = await _repository.GetImageById(id);
                if (image == null) throw new DuplicateException("ImageID", $"Không tồn tại ID {id}");
                image.ImageUrl = imageURL;
                return await _repository.UpdateImage(image);
            }
            catch
            {
                throw;
            }
        }
    }
}
