using Microsoft.EntityFrameworkCore;
using Parking.Domain.Entities;
using Parking.Infrastructure.Mappings;

namespace Parking.Infrastructure.Context;

public class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<ParkingSession> ParkingSessions => Set<ParkingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new ParkingSpotMap());
        modelBuilder.ApplyConfiguration(new ParkingSessionMap());

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.OwnsOne(v => v.LicensePlate, lp =>
            {
                lp.Property(p => p.Value)
                  .HasColumnName("LicensePlate")
                  .HasMaxLength(10)
                  .IsRequired();
            });
        });
    }
}
