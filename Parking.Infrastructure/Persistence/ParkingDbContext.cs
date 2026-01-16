using Microsoft.EntityFrameworkCore;
using Parking.Domain.Entities;

namespace Parking.Infrastructure.Persistence;

public class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    public DbSet<ParkingSession> ParkingSessions => Set<ParkingSession>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
}
