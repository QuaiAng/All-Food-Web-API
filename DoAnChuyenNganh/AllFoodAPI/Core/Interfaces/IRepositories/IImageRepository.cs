using AllFoodAPI.Core.Entities;
using System.Drawing;

namespace AllFoodAPI.Core.Interfaces.IRepository
{
    public interface IImageRepository
    {
        Task<IEnumerable<Entities.Image>> GetAllImages();
        Task<Entities.Image?> GetImageById(int id);
        Task <bool> AddImage(Entities.Image image);
        Task<bool> DeleteImage(int id);
        Task<bool> UpdateImage(Entities.Image image);
    }
}
