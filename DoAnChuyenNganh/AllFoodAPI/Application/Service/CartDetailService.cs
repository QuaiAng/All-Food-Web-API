using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.Infrastructure.Repositories;
using System.Runtime.Intrinsics.X86;

namespace AllFoodAPI.Application.Service
{
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository _cartDetailRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IProductRepository _productRepository;

        public CartDetailService(ICartDetailRepository cartDetailRepository, IShopRepository shopRepository, IProductRepository productRepository)
        {
            _cartDetailRepository = cartDetailRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
        }
        public async Task<bool> AddCartDetail(CartDetailDTO cartDetail)
        {
            try
            {
                if (await _productRepository.GetProductById(cartDetail.ProductId) == null)
                    throw new DuplicateException("ProductID", $"Không tồn tại sản phẩm có ID {cartDetail.ProductId}");
                if (await _shopRepository.GetShopById(cartDetail.ShopId) == null)
                    throw new DuplicateException("ShopID", $"Không tồn tại shop có ID {cartDetail.ShopId}");
                return await _cartDetailRepository.AddCartDetail(CartDetailDTO.ToEntity(cartDetail));
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeteleCartDetail(int id)
        {
            try
            {
                if (await _cartDetailRepository.GetCartDetailByProductId(id) == null)
                    throw new DuplicateException("CartDetail", "Không tồn tại sản phẩm này");
                return await _cartDetailRepository.DeteleCartDetail(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateCartDetail(int quantity, int id)
        {
            try
            {
                var cartDetail = await _cartDetailRepository.GetCartDetailByProductId(id);
                if (cartDetail == null) throw new DuplicateException("Product", $"Không tồn tại cart detail có ID {id}");
                cartDetail.Quantity = quantity;
                return await _cartDetailRepository.UpdateCartDetail(cartDetail);
            }
            catch
            {
                throw;
            }
        }
    }
}
