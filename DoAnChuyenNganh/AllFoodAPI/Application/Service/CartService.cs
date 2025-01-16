using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.Infrastructure.Repositories;
using AllFoodAPI.WebApi.Models.Cart;
using NuGet.Protocol.Core.Types;

namespace AllFoodAPI.Application.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly IUserRepository _userRepository;

        public CartService(ICartRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddCart(AddCartModel cartDTO)
        {
            try
            {
                if (await _userRepository.GetUserById(cartDTO.UserId) == null) throw new DuplicateException("User ID", "User ID không tồn tại");
                if(await _repository.IsUserHasCart(cartDTO.UserId)) throw new DuplicateException("Cart", $"User có ID là {cartDTO.UserId} đã tồn tại giỏ hàng");
                var cart = new Core.Entities.Cart
                {
                    UserId = cartDTO.UserId, 
                    Total = cartDTO.Total
                };
                return await _repository.AddCart(cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<CartDTO?> GetCartByUserId(int userId)
        {
            try
            {
                var cart = await _repository.GetCartByUserId(userId);
                if (cart == null)
                {
                    return null;
                }
                var cartDTO = CartDTO.FromEntity(cart);
                return cartDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }
    }
}
