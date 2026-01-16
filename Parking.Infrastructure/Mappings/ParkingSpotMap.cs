using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Mappings;

public class ParkingSpotMap : IEntityTypeConfiguration<ParkingSpot>
{
    public void Configure(EntityTypeBuilder<ParkingSpot> builder)
    {
        builder.ToTable("ParkingSpots");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(p => p.Type)
               .IsRequired();

        builder.Property(p => p.IsOccupied)
               .IsRequired();
    }
}
