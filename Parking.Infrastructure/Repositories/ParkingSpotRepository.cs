using Parking.Application.DTOs;
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
                p.IsActive &&
                !p.IsOccupied &&
                p.Type == type);
    }

    public async Task<List<ParkingSpot>> GetAvailableByTypeAsync(ParkingSpotType type)
    {
        return await _context.ParkingSpots
            .Where(s => s.Type == type && !s.IsOccupied)
            .OrderBy(s => s.Code)
            .ToListAsync();
    }

    public async Task<List<ParkingSpot>> GetAllByTypeAsync(ParkingSpotType type)
    {
        return await _context.ParkingSpots
            .Where(x => x.Type == type)
            .ToListAsync();
    }

    public async Task<ParkingSpotSummary> GetSummaryAsync(ParkingSpotType type)
    {
        var spots = await _context.ParkingSpots
            .Where(x => x.Type == type && x.IsActive)
            .ToListAsync();

        return new ParkingSpotSummary
        {
            Total = spots.Count,
            Free = spots.Count(x => !x.IsOccupied),
            Occupied = spots.Count(x => x.IsOccupied)
        };
    }
}
