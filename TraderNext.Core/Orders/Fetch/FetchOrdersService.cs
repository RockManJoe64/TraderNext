using System.Collections.Generic;
using System.Threading.Tasks;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Repository;

namespace TraderNext.Core.Orders.Fetch
{
    public class FetchOrdersService : IFetchOrdersService
    {
        private readonly IOrderRepository _orderRepository;

        public FetchOrdersService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> FetchOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders;
        }
    }
}
