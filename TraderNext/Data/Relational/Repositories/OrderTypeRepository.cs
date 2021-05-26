using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Repository;

namespace TraderNext.Data.Relational.Repositories
{
    public class OrderTypeRepository : SqlRepository<OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrderType>> GetAllOrderTypesAsync()
        {
            var orderTypes = await Query.ToListAsync();

            return orderTypes;
        }

        public async Task<Order> EnrichOrderTypeFieldAsync(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderType?.Code) || order.OrderType?.ID == 0L)
            {
                return order;
            }

            var orderTypes = await GetAllOrderTypesAsync();

            var orderType = orderTypes.SingleOrDefault(ot => ot.Code == order.OrderType?.Code);

            order.OrderType = orderType;

            return order;
        }
    }
}
