using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderNext.Core.Models;

namespace TraderNext.Data.Relational.EntityConfiguration
{
    class OrderTypeConfiguration : IEntityTypeConfiguration<OrderType>
    {
        public void Configure(EntityTypeBuilder<OrderType> builder)
        {
            builder.HasKey(o => o.ID);

            builder.Property(o => o.Code)
                .HasColumnType(SqlDataTypes.ShortCodeType)
                .IsRequired();

            builder.Property(o => o.Description)
                .HasColumnType(SqlDataTypes.LongDescriptionType)
                .IsRequired(false);
        }
    }
}
