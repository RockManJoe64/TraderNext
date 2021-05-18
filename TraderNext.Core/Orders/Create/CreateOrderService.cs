using System.Threading.Tasks;
using FluentValidation;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Repository;

namespace TraderNext.Core.Orders.Create
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OrderValidator _validator;

        public CreateOrderService(IOrderRepository orderRepository,
            OrderValidator validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _validator.ValidateAndThrow(order);

            order = await _orderRepository.CreateOrderAsync(order);

            return order;
        }
    }
}
