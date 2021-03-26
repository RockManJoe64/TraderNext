namespace TraderNext.Orders.Create
{
    public class CreateOrderRequest
    {
        public string OrderId { get; set; }

        public string Symbol { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
    }
}
