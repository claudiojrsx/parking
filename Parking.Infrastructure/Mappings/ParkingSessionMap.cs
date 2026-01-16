using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Mappings;

public class ParkingSessionMap : IEntityTypeConfiguration<ParkingSession>
{
    public void Configure(EntityTypeBuilder<ParkingSession> builder)
    {
        builder.ToTable("ParkingSessions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.EntryTime)
               .IsRequired();

        builder.Property(p => p.ExitTime);

        builder.HasIndex(p => p.VehicleId)
               .IsUnique(false);
    }
}
