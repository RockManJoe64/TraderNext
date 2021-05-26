using AutoMapper;
using TraderNext.Core.Models;

namespace TraderNext.Data.Mapping
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAuditFields<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
                where TDestination : IAuditable
        {
            return expression
                .ForMember(dest => dest.CreatedBy, opts => opts.Ignore())
                .ForMember(dest => dest.CreatedTime, opts => opts.Ignore())
                .ForMember(dest => dest.ModifiedBy, opts => opts.Ignore())
                .ForMember(dest => dest.ModifiedTime, opts => opts.Ignore());
        }
    }
}
