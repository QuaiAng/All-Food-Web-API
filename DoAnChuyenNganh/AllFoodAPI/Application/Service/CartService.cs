using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using NuGet.Protocol.Core.Types;

namespace AllFoodAPI.Application.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
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
