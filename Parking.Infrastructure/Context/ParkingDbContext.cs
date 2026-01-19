using Microsoft.EntityFrameworkCore;
using Parking.Domain.Entities;
using Parking.Infrastructure.Entities;
using Parking.Infrastructure.Mappings;
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
    public DbSet<PricingConfiguration> PricingConfigurations => Set<PricingConfiguration>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Mapeamentos separados (boa prática)
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new ParkingSpotMap());
        modelBuilder.ApplyConfiguration(new ParkingSessionMap());

        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());

        modelBuilder.Entity<Role>().HasData(
            new Role("Admin")
            {
                Id = AuthSeed.AdminRoleId
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User(
                "System Admin",
                "admin@parking.com",
                AuthSeed.AdminPasswordHash,
                AuthSeed.AdminRoleId
            )
            {
                Id = AuthSeed.AdminUserId
            }
        );

        modelBuilder.Entity<PricingConfiguration>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.CarHourlyRate)
                  .HasPrecision(10, 2);

            entity.Property(x => x.MotorcycleHourlyRate)
                  .HasPrecision(10, 2);

            entity.Property(x => x.TruckHourlyRate)
                  .HasPrecision(10, 2);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParkingDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
