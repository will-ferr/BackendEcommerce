using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(p => p.Price)
            .HasColumnName("price")
            .HasColumnType("numeric(10,2)")
            .IsRequired()
            .HasConversion(
                money => money.Amount,
                value => Money.Create(value)
            );
        
        builder.Property(p => p.StockQuantity)
            .HasColumnName("stock_quantity")
            .IsRequired();
        
        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();
        
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(p =>p.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
    }
}