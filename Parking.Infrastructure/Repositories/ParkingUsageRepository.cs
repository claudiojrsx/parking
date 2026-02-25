using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class ParkingUsageRepository : IParkingUsageRepository
{
    private readonly ParkingDbContext _context;

    public ParkingUsageRepository(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<ParkingUsage?> GetActiveByVehicleAsync(Guid vehicleId)
    {
        return await _context.ParkingUsages
            .FirstOrDefaultAsync(p => p.VehicleId == vehicleId && p.IsActive);
    }

    public async Task<ParkingUsage?> GetActiveBySpotAsync(Guid parkingSpotId)
    {
        return await _context.ParkingUsages
            .FirstOrDefaultAsync(p => p.ParkingSpotId == parkingSpotId && p.IsActive);
    }

    public async Task<ParkingUsage?> GetByIdAsync(Guid id)
    {
        return await _context.ParkingUsages
            .Include(p => p.Vehicle)
            .Include(p => p.ParkingSpot)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(ParkingUsage usage)
    {
        _context.ParkingUsages.Add(usage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ParkingUsage usage)
    {
        _context.ParkingUsages.Update(usage);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ParkingUsage>> GetAllActiveAsync()
    {
        return await _context.ParkingUsages
            .Include(x => x.Vehicle)
            .Include(x => x.ParkingSpot)
            .Where(x => x.IsActive)
            .ToListAsync();
    }
}
