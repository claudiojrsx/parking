using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Entities;

public class ParkingUsageConfiguration : IEntityTypeConfiguration<ParkingUsage>
{
    public void Configure(EntityTypeBuilder<ParkingUsage> builder)
    {
        builder.ToTable("ParkingUsages");

        builder.HasKey(pu => pu.Id);

        builder.Property(pu => pu.UsageType)
            .IsRequired();

        builder.Property(pu => pu.IsActive)
            .IsRequired();

        builder.Property(pu => pu.CreatedAt)
            .IsRequired();

        // Daily
        builder.Property(pu => pu.EntryTime);
        builder.Property(pu => pu.ExitTime);

        // Monthly
        builder.Property(pu => pu.StartDate);
        builder.Property(pu => pu.EndDate);

        builder.HasOne(pu => pu.ParkingSpot)
            .WithMany()
            .HasForeignKey(pu => pu.ParkingSpotId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pu => pu.Vehicle)
            .WithMany()
            .HasForeignKey(pu => pu.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índice importante
        builder.HasIndex(pu => new { pu.VehicleId, pu.IsActive });
        builder.HasIndex(pu => new { pu.ParkingSpotId, pu.IsActive });
    }
}
