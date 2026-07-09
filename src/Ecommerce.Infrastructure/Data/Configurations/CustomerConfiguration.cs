using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customer");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name) 
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsRequired()
            .HasConversion(
                email => email.Value,         // fazendo o FW entender o email como uma string.
                value => Email.Create(value)  // lendo a string como se fosse um email.
            );
        
        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(c => c.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
    }
}