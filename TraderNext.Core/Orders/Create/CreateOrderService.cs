using System.Threading.Tasks;
using FluentValidation;
using TraderNext.Core.Orders.Repository;
using TraderNext.Models;

namespace TraderNext.Orders.Create
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CreateOrderRequestValidator _validator;

        public CreateOrderService(IOrderRepository orderRepository,
            CreateOrderRequestValidator validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
        {
            _validator.ValidateAndThrow(request);

            var order = request.Order;

            order = await _orderRepository.CreateOrderAsync(order);

            return order;
        }
    }
}
