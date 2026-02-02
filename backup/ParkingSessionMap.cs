using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Mappings;

public class ParkingSessionMap : IEntityTypeConfiguration<ParkingSession>
{
    public void Configure(EntityTypeBuilder<ParkingSession> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.EntryTime)
               .IsRequired();

        builder.Property(s => s.ExitTime);

        builder.HasIndex(s => new { s.VehicleId, s.ExitTime });
    }
}
