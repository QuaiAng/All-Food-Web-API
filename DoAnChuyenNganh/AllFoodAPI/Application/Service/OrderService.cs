using AllFoodAPI.Core.DTOs;
using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Exceptions;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Core.Interfaces.IRepository;
using AllFoodAPI.Core.Interfaces.IServices;
using AllFoodAPI.WebApi.Models.Order;
using NuGet.Protocol.Core.Types;

namespace AllFoodAPI.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public async Task<bool> AddOrder(AddOrderModel order)
        {
            try
            {
                var orderAdd = new Order
                {

                    Total = order.Total,
                    DeliveryAddress = order.DeliveryAddress,
                    PaymentMethod = order.PaymentMethod,
                    Discount = order.Discount,
                    FullNameUser = order.FullNameUser,
                    ShopName = order.ShopName,
                    PhoneNum = order.PhoneNum,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    UserId = order.UserId,
                    OrderStatus = 0,
                    Status = true,
                    OrderDetails = order.OrderDetails.Select(u => new OrderDetail
                    {
                        OrderId = u.OrderId,
                        ProductId = u.ProductId,
                        Note = u.Note,
                        Price = u.Price,
                        ProductName = u.ProductName,
                        Quantity = u.Quantity,

                    }).ToList(),

                };
                return await _repository.AddOrder(orderAdd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _repository.GetOrderById(orderId);
                if (order == null) throw new DuplicateException("Order", "Không tìm thấy order này");
                return await _repository.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            try
            {
                var orders = await _repository.GetAllOrders();
                var orderDTO = orders.Select(u => OrderDTO.FromEntity(u));
                return orderDTO;
            }
            catch { throw; }
        }

        public async Task<OrderDTO?> GetOrderById(int orderId)
        {
            try
            {
                var order = await _repository.GetOrderById(orderId);
                if (order == null)
                {
                    return null;
                }
                var orderDTO = OrderDTO.FromEntity(order);
                return orderDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserId(int userId)
        {
            try
            {
                if (await _userRepository.GetUserById(userId) == null) throw new DuplicateException("User ID", "User ID không tồn tại");
                var orders = await _repository.GetOrdersByUserId(userId);

                var orderDTOs = orders.Select(u => OrderDTO.FromEntity(u));
                return orderDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw new ApplicationException("Xảy ra lỗi khi truy vấn", ex);
            }
        }

        public async Task<bool> UpdateOrder(int orderId, int userId, int orderStatus)
        {
            try
            {
                if (await _userRepository.GetUserById(userId) == null) throw new DuplicateException("User ID", "User ID không tồn tại");
                var orderUpdate = await _repository.GetOrderById(orderId);
                if (orderUpdate == null)
                {
                    return false;
                }

                orderUpdate.OrderStatus = orderStatus;
                return await _repository.UpdateOrder(orderUpdate);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }
    }
}
