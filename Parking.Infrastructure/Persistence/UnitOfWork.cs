using Parking.Application.Interfaces.Persistence;

namespace Parking.Infrastructure.Persistence;

public class UnitOfWork(ParkingDbContext context) : IUnitOfWork
{
    private readonly ParkingDbContext _context = context;

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
