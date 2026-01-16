using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Domain.ValueObjects;
using Parking.Infrastructure.Persistence;

namespace Parking.Infrastructure.Repositories;

public class ParkingSessionRepository 
    : Repository<ParkingSession>, IParkingSessionRepository
{
    public ParkingSessionRepository(ParkingDbContext context)
        : base(context)
    {
    }

    public async Task<ParkingSession?> GetActiveSessionByPlateAsync(string plate)
    {
        var licensePlate = new LicensePlate(plate);

        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

        if (vehicle == null)
            return null;

        return await _context.ParkingSessions
            .FirstOrDefaultAsync(s =>
                s.VehicleId == vehicle.Id &&
                s.ExitTime == null);
    }

    public async Task<IEnumerable<ParkingSession>> GetByPeriodAsync(
        DateTime start, DateTime end)
    {
        return await _context.ParkingSessions
            .Where(x => x.EntryTime >= start && x.EntryTime <= end)
            .ToListAsync();
    }
}
