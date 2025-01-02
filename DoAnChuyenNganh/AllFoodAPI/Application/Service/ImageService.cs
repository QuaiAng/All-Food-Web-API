using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IService;
using AllFoodAPI.Infrastructure.Repositories;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace AllFoodAPI.Application.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        private readonly IProductRepository _productRepository;

        public ImageService(IImageRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }
        public async Task<bool> AddImage(ImageDTO image)
        {
            try
            {
                if (await _productRepository.GetProductById(image.ProductId) == null)
                    throw new DuplicateException("ProductID", $"Không tồn tại sản phẩm có ID {image.ProductId}");
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
                var image = await _repository.GetImageById(id);
                if (image == null) return null;
                return ImageDTO.FromEntity(image);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ImageDTO?>> GetImageByProductId(int productId)
        {
            try
            {
                var images = await _repository.GetImageByProductId(productId);
                var imageDTOs = images.Select(u => ImageDTO.FromEntity(u));
                return imageDTOs;
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
