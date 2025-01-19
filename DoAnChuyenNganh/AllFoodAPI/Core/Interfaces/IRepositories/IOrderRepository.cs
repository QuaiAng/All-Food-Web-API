using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.WebApi.Models.Product;

namespace AllFoodAPI.Core.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
        Task<IEnumerable<Order>> GetOrdersByShopId(int shopId);
        Task<bool> AddOrder(Order order);
        Task<bool> UpdateOrder(Order order);
        Task<bool> DeleteOrder(int orderId);
    }
}
