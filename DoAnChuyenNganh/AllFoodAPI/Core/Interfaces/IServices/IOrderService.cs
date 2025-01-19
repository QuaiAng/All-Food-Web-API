using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Order;
using AllFoodAPI.WebApi.Models.Product;

namespace AllFoodAPI.Core.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<IEnumerable<OrderDTO>> GetOrdersByUserId(int userId);
        Task<IEnumerable<OrderDTO>> GetOrdersByShopId(int shopId);

        Task<OrderDTO?> GetOrderById(int orderId);
        Task<bool> AddOrder(AddOrderModel order);
        Task<bool> UpdateOrder(int orderId, int userId, int orderStatus);
        Task<bool> DeleteOrder(int orderId);
    }
}
