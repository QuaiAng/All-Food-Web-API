using AllFoodAPI.Core.DTOs;

namespace AllFoodAPI.Core.Interfaces.IService
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDTO>> GetAllImages();
        Task<ImageDTO?> GetImageById(int id);
        Task<bool> AddImage(ImageDTO image);
        Task<bool> DeleteImage(int id);
        Task<bool> UpdateImage(string imageURL, int id);
    }
}
