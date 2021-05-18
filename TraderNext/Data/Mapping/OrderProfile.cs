using AutoMapper;
using TraderNext.Api;

namespace TraderNext.Data.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, Core.Models.Order>()
                .ReverseMap();
        }
    }
}
