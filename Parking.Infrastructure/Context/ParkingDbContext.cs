using Microsoft.EntityFrameworkCore;
using Parking.Domain.Entities;
using Parking.Infrastructure.Entities;
using Parking.Infrastructure.Seeds;

namespace Parking.Infrastructure.Context;

public class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<ParkingSession> ParkingSessions => Set<ParkingSession>();
    public DbSet<Pricing> Pricings => Set<Pricing>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica TODAS as IEntityTypeConfiguration automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParkingDbContext).Assembly);

        // Seeds de Roles
        modelBuilder.Entity<Role>().HasData(
            new Role("Admin") { Id = AuthSeed.AdminRoleId },
            new Role("Operator") { Id = AuthSeed.OperatorRoleId },
            new Role("Attendant") { Id = AuthSeed.AttendantRoleId }
        );

        // Seed do usuário admin
        modelBuilder.Entity<User>().HasData(
            new User(
                "System Admin",
                "admin@parking.com",
                AuthSeed.AdminPasswordHash,
                AuthSeed.AdminRoleId,
                true
            )
            {
                Id = AuthSeed.AdminUserId
            }
        );

        modelBuilder.ApplyConfiguration(new ParkingUsageConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
