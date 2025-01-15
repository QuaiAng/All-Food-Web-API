using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.Infrastructure.Repositories;
using AllFoodAPI.WebApi.Models.Cart;
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
        public async Task<bool> AddCartDetail(AddCartDetailModel addCartDetail)
        {
            try
            {
                if (await _productRepository.GetProductById(addCartDetail.ProductId) == null)
                    throw new DuplicateException("ProductID", $"Không tồn tại sản phẩm có ID {addCartDetail.ProductId}");
                if (await _shopRepository.GetShopById(addCartDetail.ShopId) == null)
                    throw new DuplicateException("ShopID", $"Không tồn tại shop có ID {addCartDetail.ShopId}");
                if (await _cartDetailRepository.IsExistProductInCart(addCartDetail.ProductId, addCartDetail.CartId))
                {
                    var cartDetailUpdate = await _cartDetailRepository.GetCartDetailByProductId(addCartDetail.ProductId, addCartDetail.CartId);
                    if (cartDetailUpdate != null)
                        cartDetailUpdate.Quantity = (cartDetailUpdate.Quantity + addCartDetail.Quantity);
                    return await _cartDetailRepository.UpdateCartDetail(cartDetailUpdate!);
                }
                   
                var cartDetail = new CartDetail
                {
                    CartId = addCartDetail.CartId,
                    ProductId = addCartDetail.ProductId,
                    Quantity = addCartDetail.Quantity,
                    ShopId = addCartDetail.ShopId,
                };
                return await _cartDetailRepository.AddCartDetail(cartDetail);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteCartDetail(int productId, int cartId)
        {
            try
            {
                if (await _cartDetailRepository.GetCartDetailByProductId(productId, cartId) == null)
                    throw new DuplicateException("CartDetail", "Không tồn tại sản phẩm này");
                return await _cartDetailRepository.DeteleCartDetail(productId, cartId);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateCartDetail(int quantity, int productId, int cartId)
        {
            try
            {
                var cartDetail = await _cartDetailRepository.GetCartDetailByProductId(productId, cartId);
                if (cartDetail == null) throw new DuplicateException("Cartdetail", $"Không tồn tại cart detail có productID là {productId} và cartID là {cartId}");
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
