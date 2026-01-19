using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Context;

public class ParkingDbContextFactory
    : IDesignTimeDbContextFactory<ParkingDbContext>
{
    public ParkingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ParkingDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=192.168.0.110;Database=COBwebTeste;User Id=sa;Password=masterrdpt;TrustServerCertificate=True"
        );

        return new ParkingDbContext(optionsBuilder.Options);
    }
}
