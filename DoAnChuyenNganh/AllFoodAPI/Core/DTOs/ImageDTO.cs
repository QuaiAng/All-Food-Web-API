using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class ImageDTO
    {
        public int ImageId { get; set; }

        public int ProductId { get; set; }

        public string ImageUrl { get; set; } = null!;


        public static ImageDTO FromEntity(Entities.Image image)
        {
            return new ImageDTO
            {
                ImageId = image.ImageId,
                ProductId = image.ProductId,
                ImageUrl = image.ImageUrl,
            };
        }
        public static Entities.Image ToEntity(ImageDTO imageDTO)
        {
            return new Entities.Image
            {
                ImageId = imageDTO.ImageId,
                ProductId = imageDTO.ProductId,
                ImageUrl = imageDTO.ImageUrl,
            };
        }
    }
}
