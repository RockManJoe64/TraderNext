using FluentValidation;

namespace TraderNext.Orders.Create
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(o => o.Symbol).NotEmpty();
            RuleFor(o => o.Quantity).NotEmpty();
            RuleFor(o => o.Price).NotEmpty();
        }
    }
}
