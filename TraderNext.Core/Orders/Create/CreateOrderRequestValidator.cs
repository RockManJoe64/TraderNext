using FluentValidation;

namespace TraderNext.Core.Orders.Create
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(o => o.Order).NotNull();
            When(o => o.Order != null, () =>
            {
                RuleFor(o => o.Order.OrderId).NotEmpty();
                RuleFor(o => o.Order.Symbol).NotEmpty();
                RuleFor(o => o.Order.Quantity).NotEmpty();
                RuleFor(o => o.Order.Price).NotEmpty();
            });
        }
    }
}
