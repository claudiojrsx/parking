using Microsoft.EntityFrameworkCore;
using Parking.Domain.Entities;
using Parking.Infrastructure.Seeds;

namespace Parking.Infrastructure.Context;

public class ParkingDbContext : DbContext
{
    public ParkingDbContext(DbContextOptions<ParkingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<ParkingSession> ParkingSessions => Set<ParkingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParkingDbContext).Assembly);

        modelBuilder.Entity<ParkingSpot>()
            .HasData(ParkingSpotSeed.Get());
    }
}
