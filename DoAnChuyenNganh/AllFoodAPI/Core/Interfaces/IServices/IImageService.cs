using AllFoodAPI.Core.DTOs;
using System.Runtime.Intrinsics.X86;

namespace AllFoodAPI.Core.Interfaces.IService
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDTO>> GetAllImages();
        Task<ImageDTO?> GetImageById(int id);
        Task<bool> AddImage(ImageDTO image);
        Task<bool> DeleteImage(int id);
        Task<bool> UpdateImage(string imageURL, int id);
        Task<IEnumerable<ImageDTO?>> GetImageByProductId(int productId);
    }
}
