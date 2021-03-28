using TraderNext.Models;

namespace TraderNext.Core.Orders.Create
{
    public class CreateOrderRequest
    {
        public Order Order { get; set; }

        public CreateOrderRequest()
        {
        }

        public CreateOrderRequest(Order order)
        {
            Order = order;
        }
    }
}
