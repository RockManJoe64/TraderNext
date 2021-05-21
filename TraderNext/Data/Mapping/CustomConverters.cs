using System;
using AutoMapper;

namespace TraderNext.Data.Mapping
{
    public class DateTimeConverter : ITypeConverter<DateTime, DateTimeOffset>
    {
        public DateTimeOffset Convert(DateTime source, DateTimeOffset destination, ResolutionContext context)
        {
            return new DateTimeOffset(source);
        }
    }

    public class DateTimeOffsetConverter : ITypeConverter<DateTimeOffset, DateTime>
    {
        public DateTime Convert(DateTimeOffset source, DateTime destination, ResolutionContext context)
        {
            return source.DateTime;
        }
    }
}
