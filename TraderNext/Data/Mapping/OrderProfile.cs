using AutoMapper;
using TraderNext.Api;
using OrderModel = TraderNext.Core.Models.Order;
using OrderTypeModel = TraderNext.Core.Models.OrderType;

namespace TraderNext.Data.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>()
                .IgnoreAuditFields();

            CreateMap<OrderModel, Order>();

            CreateMap<OrderType, OrderTypeModel>()
                .ForMember(d => d.Code, opts => opts.MapFrom(s => s.ToString()))
                .ForMember(d => d.ID, opts => opts.Ignore())
                .ForMember(d => d.Description, opts => opts.Ignore());

            CreateMap<OrderTypeModel, OrderType>()
                .ConvertUsing(model => (OrderType)System.Enum.Parse(typeof(OrderType), model.Code));
        }
    }
}
