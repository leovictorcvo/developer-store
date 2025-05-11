using System.Diagnostics.CodeAnalysis;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Configuration for the User entity.
/// </summary>
[ExcludeFromCodeCoverage]
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.FirstName).IsRequired().HasColumnName("FirstName").HasMaxLength(50);
            name.Property(n => n.LastName).IsRequired().HasColumnName("LastName").HasMaxLength(50);
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.City).IsRequired().HasColumnName("City").HasMaxLength(50);
            address.Property(a => a.Street).IsRequired().HasColumnName("Street").HasMaxLength(100);
            address.Property(a => a.Number).IsRequired().HasColumnName("Number");
            address.Property(a => a.ZipCode).IsRequired().HasColumnName("ZipCode").HasMaxLength(10);

            address.OwnsOne(a => a.Geolocation, geo =>
            {
                geo.Property(g => g.Lat).IsRequired().HasColumnName("Latitude").HasMaxLength(15);
                geo.Property(g => g.Long).IsRequired().HasColumnName("Longitude").HasMaxLength(15);
            });
        });

        builder.HasIndex(u => u.Email).IsUnique().HasDatabaseName("UX_Email");
    }
}