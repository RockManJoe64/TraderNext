﻿using System.Threading.Tasks;
using FluentValidation;
using TraderNext.Models;
using TraderNext.Orders.Repository;

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

            var order = new Order
            {
                OrderId = request.OrderId,
                Symbol = request.Symbol,
                Quantity = request.Quantity,
                Price = request.Price,
            };

            order = await _orderRepository.CreateOrderAsync(order);

            return order;
        }
    }
}
