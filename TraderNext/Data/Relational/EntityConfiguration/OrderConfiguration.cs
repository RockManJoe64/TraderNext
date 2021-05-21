using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TraderNext.Core.Models;

namespace TraderNext.Data.Relational.EntityConfiguration
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.ID);

            builder.Property(o => o.OrderId)
                .HasColumnType(SqlDataTypes.ComplexIdType)
                .IsRequired();

            builder.Property(o => o.SecondaryOrderId)
                .HasColumnType(SqlDataTypes.ComplexIdType)
                .IsRequired();

            builder.Property(o => o.Symbol)
                .HasColumnType(SqlDataTypes.ShortCodeType)
                .IsRequired();

            builder.Property(o => o.Quantity)
                .HasColumnType<int>(SqlDataTypes.QuantityType)
                .IsRequired();

            builder.Property(o => o.Price)
                .HasColumnType<decimal>(SqlDataTypes.PriceType)
                .IsRequired();

            builder.Property(o => o.StopPrice)
                .HasColumnType<decimal>(SqlDataTypes.PriceType)
                .IsRequired(false);

            builder.Property(o => o.Amount)
                .HasColumnType<decimal>(SqlDataTypes.AmountType)
                .IsRequired();

            builder.Property(o => o.EffectiveTime)
                .HasColumnType<DateTime>(SqlDataTypes.TimestampType)
                .IsRequired();

            builder.Property(o => o.ExpireTime)
                .HasColumnType<DateTime>(SqlDataTypes.TimestampType)
                .IsRequired();

            builder.Property(o => o.TransactionTime)
                .HasColumnType<DateTime>(SqlDataTypes.TimestampType)
                .IsRequired();

            builder.Property(o => o.TradeDate)
                .HasColumnType<DateTime>(SqlDataTypes.DateType)
                .IsRequired();

            builder.Property(o => o.SettleDate)
                .HasColumnType<DateTime>(SqlDataTypes.DateType)
                .IsRequired();
        }
    }
}
