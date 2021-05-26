using System;

namespace TraderNext.Core.Models
{
    public sealed class Order : IAuditable
    {
        public long ID { get; set; }

        public string OrderId { get; set; }

        public string SecondaryOrderId { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal StopPrice { get; set; }

        public decimal Amount { get; set; }

        public DateTime EffectiveTime { get; set; }

        public DateTime ExpireTime { get; set; }

        public DateTime TransactionTime { get; set; }

        public DateTime TradeDate { get; set; }

        public DateTime SettleDate { get; set; }

        public DateTime CreatedTime { get; set; }

        public long CreatedBy { get; set; }
        
        public DateTime ModifiedTime { get; set; }
     
        public long ModifiedBy { get; set; }

        public OrderType OrderType { get; set; }
    }
}
