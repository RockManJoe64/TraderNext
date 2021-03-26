using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderNext.Models;

namespace TraderNext.Data.Relational.EntityConfiguration
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.ID);

            builder.Property(o => o.OrderId)
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.Property(o => o.Symbol)
                .HasColumnType("varchar(6)")
                .IsRequired();

            builder.Property(o => o.Quantity)
                .HasColumnType<int>("number(12,0)")
                .IsRequired(true);

            builder.Property(o => o.Price)
                .HasColumnType<decimal>("number(8,2)")
                .IsRequired(true);
        }
    }
}
