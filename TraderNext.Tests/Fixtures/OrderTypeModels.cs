using System.Collections.Generic;
using AutoFixture;
using TraderNext.Core.Models;

namespace TraderNext.Tests.Fixtures
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

        public static OrderType Market() => CreateOrderType(Api.OrderType.Market);

        public static OrderType Limit() => CreateOrderType(Api.OrderType.Limit);

        public static OrderType Stop() => CreateOrderType(Api.OrderType.Stop);
        
        public static OrderType StopLimit() => CreateOrderType(Api.OrderType.Stoplimit);

        private static OrderType CreateOrderType(Api.OrderType orderType) => _fixture.Build<OrderType>().With(o => o.Code, orderType.ToString()).Create();
    }
}
