using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Domain.Enums;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class ParkingSpotRepository
    : Repository<ParkingSpot>, IParkingSpotRepository
{
    public ParkingSpotRepository(ParkingDbContext context)
        : base(context)
    {
    }

    public async Task<ParkingSpot?> GetAvailableAsync(ParkingSpotType type)
    {
        return await _context.ParkingSpots
            .FirstOrDefaultAsync(p =>
                !p.IsOccupied &&
                p.Type == type);
    }
}
