using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Configuration for the Product entity.
/// </summary>
[ExcludeFromCodeCoverage]
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
        builder.Property(p => p.Category).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Image).IsRequired().HasMaxLength(100);

        builder.OwnsOne(p => p.Rating, rating =>
        {
            rating.Property(r => r.Rate).IsRequired().HasColumnType("decimal(4,2)");
            rating.Property(r => r.Count).IsRequired();
        });

        builder.HasIndex(p => p.Title).IsUnique().HasDatabaseName("UX_Title");
        builder.HasIndex(p => p.Category).IsUnique(false).HasDatabaseName("IX_Category");
    }
}