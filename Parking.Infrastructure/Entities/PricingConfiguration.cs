using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Entities;

public class PricingMap : IEntityTypeConfiguration<Pricing>
{
    public void Configure(EntityTypeBuilder<Pricing> builder)
    {
        builder.ToTable("Pricings");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.MotorcycleHourlyRate)
               .IsRequired()
               .HasPrecision(10, 2);

        builder.Property(p => p.CarHourlyRate)
               .IsRequired()
               .HasPrecision(10, 2);

        builder.Property(p => p.TruckHourlyRate)
               .IsRequired()
               .HasPrecision(10, 2);

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.IsActive)
               .IsRequired();

        // Regra importante: só uma Pricing ativa
        builder.HasIndex(p => p.IsActive)
               .HasFilter("[IsActive] = 1")
               .IsUnique();
    }
}
