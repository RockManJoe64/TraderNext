using System.Collections.Generic;
using System.Threading.Tasks;
using TraderNext.Core.Orders.Repository;
using TraderNext.Models;

namespace TraderNext.Orders.Fetch
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
