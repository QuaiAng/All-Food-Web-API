using AllFoodAPI.Core.Entities;

namespace AllFoodAPI.Core.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int Price { get; set; }

        public string? Description { get; set; }

        public int ShopId { get; set; }

        public int CategoryId { get; set; }

        public int? SalesCount { get; set; }

        public int Available { get; set; }
        public int? Rating { get; set; }
        public string? ImageUrl { get; set; }



        public static ProductDTO FromEntity(Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ShopId = product.ShopId,
                CategoryId = product.CategoryId,
                SalesCount = product.SalesCount,
                Available = product.Available,
                Rating = product.Rating,
                ImageUrl = product.ImageUrl,
            };
        }

        public static Product ToEnTity (ProductDTO productDTO)
        {
            return new Product
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
                Price = productDTO.Price,
                Description = productDTO.Description,
                ShopId = productDTO.ShopId,
                CategoryId = productDTO.CategoryId,
                SalesCount = productDTO.SalesCount,
                Available = productDTO.Available,
                Rating= productDTO.Rating,
                ImageUrl = productDTO.ImageUrl,
            };
        }
    }
}
