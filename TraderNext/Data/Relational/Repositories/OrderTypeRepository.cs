using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<OrderType> GetAllOrderTypes()
        {
            var orderTypes = Query.ToList();

            return orderTypes;
        }
    }
}
