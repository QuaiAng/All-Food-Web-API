using AllFoodAPI.Core.Entities;
using AllFoodAPI.Core.Interfaces.IRepositories;
using AllFoodAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AllFoodAPI.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AllfoodDbContext _context;

        public OrderRepository(AllfoodDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Xảy ra lỗi khi thêm", ex);

            }
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders.SingleOrDefaultAsync(u => u.OrderId == orderId);
                if (order == null) return false;
                order.Status = false;
                _context.Orders.Update(order);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                var orders = await _context.Orders.Where(u => u.Status == true).Include(p => p.OrderDetails).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            try
            {
                var order = await _context.Orders.Include(p => p.OrderDetails).SingleOrDefaultAsync(u => u.OrderId == orderId && u.Status == true);
                if (order == null)
                    return null;
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _context.Orders.Where(u => u.UserId == userId && u.Status == true).Include(p => p.OrderDetails).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
