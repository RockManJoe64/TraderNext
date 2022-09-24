using System.Collections.Generic;
using AutoFixture;
using TraderNext.Core.Models;

namespace TraderNext.Core.Orders.Fixtures
{
    public static class OrderTypeModels
    {
        private static readonly Fixture _fixture = new Fixture();

        public static IEnumerable<OrderType> GetAll()
        {
            return new List<OrderType>
            {
                Market(),
                Limit(),
                Stop(),
                StopLimit(),
            };
        }

        public static OrderType Market() => CreateOrderType("Market", 1);

        public static OrderType Limit() => CreateOrderType("Limit", 2);

        public static OrderType Stop() => CreateOrderType("Stop", 3);
        
        public static OrderType StopLimit() => CreateOrderType("Stoplimit", 4);

        private static OrderType CreateOrderType(string orderType, long Id) => 
            _fixture.Build<OrderType>()
                .With(o => o.Code, orderType.ToString())
                .With(o => o.ID, Id)
                .Create();
    }
}
