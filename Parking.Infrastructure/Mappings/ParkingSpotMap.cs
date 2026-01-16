using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Mappings;

public class ParkingSpotMap : IEntityTypeConfiguration<ParkingSpot>
{
    public void Configure(EntityTypeBuilder<ParkingSpot> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(20);

        builder.Property(p => p.Type)
               .IsRequired();

        builder.Property(p => p.IsOccupied)
               .IsRequired();
    }
}
