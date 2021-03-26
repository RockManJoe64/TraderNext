using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraderNext.Models;
using TraderNext.Orders.Repository;

namespace TraderNext.Data.Relational.Repositories
{
    public sealed class OrderRepository : SqlRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            var entry = DbContext.Entry(order);

            if (entry.State == EntityState.Detached)
            {
                await Set.AddAsync(order);
            }

            var writes = await DbContext.SaveChangesAsync();

            if (writes == 0)
            {
                throw new InsertUpdateException($"Expected order {order.OrderId} to be persisted");
            }

            return entry.Entity;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await Query.ToListAsync();

            return orders;
        }

        public async Task<Order> GetByOrderIdAsync(string orderId)
        {
            var order = await Query.Where(o => o.OrderId == orderId).SingleOrDefaultAsync();

            return order;
        }
    }
}
