using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class OrderItemCongiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(i => i.ProductNameSnapshot)
            .HasColumnName("product_name_snapshot")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(i => i.UnitPriceSnapshot)
            .HasColumnName("unit_price_snapshot")
            .HasColumnType("numeric(10,2)")
            .IsRequired()
            .HasConversion(
                money => money.Amount,
                value => Money.Create(value)
            );

        builder.Property(i => i.Quantity)
            .HasColumnName("quantity")
            .IsRequired();
    }
}

