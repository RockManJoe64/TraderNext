using System.Collections.Generic;
using System.Threading.Tasks;
using TraderNext.Core.Data.Relational;
using TraderNext.Core.Models;

namespace TraderNext.Core.Orders.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> CreateOrderAsync(Order order);

        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order> GetByOrderIdAsync(string orderId);
    }
}
