using TraderNext.Models;

namespace TraderNext.Orders.Create
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
