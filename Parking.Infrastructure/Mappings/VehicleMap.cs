using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Mappings;

public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.OwnsOne(v => v.LicensePlate, lp =>
        {
            lp.Property(p => p.Value)
              .HasColumnName("LicensePlate")
              .HasMaxLength(10)
              .IsRequired();
        });

        builder.Property(v => v.Type)
               .IsRequired();
    }
}
