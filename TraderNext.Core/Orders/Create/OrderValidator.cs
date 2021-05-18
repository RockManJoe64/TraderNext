using FluentValidation;
using TraderNext.Core.Models;

namespace TraderNext.Core.Orders.Create
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderId).NotEmpty();
            RuleFor(o => o.Symbol).NotEmpty();
            RuleFor(o => o.Quantity).NotEmpty();
            RuleFor(o => o.Price).NotEmpty();
        }
    }
}
