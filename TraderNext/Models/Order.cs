namespace TraderNext.Models
{
    public sealed class Order
    {
        public long ID { get; set; }

        public string OrderId { get; set; }

        public string Symbol { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
    }
}
