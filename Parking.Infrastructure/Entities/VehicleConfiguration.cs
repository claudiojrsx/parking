using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Entities;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.OwnsOne(v => v.LicensePlate, lp =>
        {
            lp.WithOwner();

            lp.Property(p => p.Value)
              .HasColumnName("LicensePlate")
              .HasMaxLength(10)
              .IsRequired();

            lp.HasIndex(p => p.Value)
              .IsUnique();
        });

        builder.Property(v => v.Type)
               .HasConversion<int>()
               .IsRequired();
    }
}
