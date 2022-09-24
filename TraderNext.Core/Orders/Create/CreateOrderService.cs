using System;
using System.Threading.Tasks;
using FluentValidation;
using TraderNext.Core.Models;
using TraderNext.Core.Orders.Repository;

namespace TraderNext.Core.Orders.Create
{
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderTypeRepository _orderTypeRepository;
        private readonly OrderValidator _validator;

        public CreateOrderService(IOrderRepository orderRepository,
            IOrderTypeRepository orderTypeRepository,
            OrderValidator validator)
        {
            _orderRepository = orderRepository;
            _orderTypeRepository = orderTypeRepository;
            _validator = validator;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _validator.ValidateAndThrow(order);

            order = CalculateAmount(order);

            order = await _orderTypeRepository.EnrichOrderTypeFieldAsync(order);

            order = await _orderRepository.CreateOrderAsync(order);

            return order;
        }

        private static Order CalculateAmount(Order order)
        {
            order.Amount = order.Price * order.Quantity; // TODO need to consider StopPrice

            return order;
        }
    }
}
