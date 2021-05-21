using System;
using AutoMapper;

namespace TraderNext.Data.Mapping
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap<DateTimeOffset, DateTime>().ConvertUsing<DateTimeOffsetConverter>();
            CreateMap<DateTime, DateTimeOffset>().ConvertUsing<DateTimeConverter>();
        }
    }
}
