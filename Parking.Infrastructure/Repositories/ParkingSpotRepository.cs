using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class ParkingSpotRepository : IParkingSpotRepository
{
    private readonly ParkingDbContext _context;

    public ParkingSpotRepository(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<ParkingSpot?> GetAvailableAsync()
    {
        return await _context.ParkingSpots
            .FirstOrDefaultAsync(p => !p.IsOccupied);
    }

    public async Task UpdateAsync(ParkingSpot parkingSpot)
    {
        _context.ParkingSpots.Update(parkingSpot);
        await _context.SaveChangesAsync();
    }
}
