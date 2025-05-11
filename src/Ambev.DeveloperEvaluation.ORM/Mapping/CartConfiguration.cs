using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Configuration for the Cart entity.
/// </summary>
[ExcludeFromCodeCoverage]
public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.Date).IsRequired();

        builder.OwnsMany(p => p.Products, cartItems =>
        {
            cartItems.ToTable("CartItems");
            cartItems.WithOwner().HasForeignKey("CartId");
            cartItems.HasKey("CartId", "ProductId");

            cartItems.Property(ci => ci.ProductId).IsRequired();
            cartItems.Property(ci => ci.Quantity).IsRequired();
        });

        builder.Metadata
            .FindNavigation(nameof(Cart.Products))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}