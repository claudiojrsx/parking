using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class ParkingSessionRepository
    : Repository<ParkingSession>, IParkingSessionRepository
{
    public ParkingSessionRepository(ParkingDbContext context)
        : base(context)
    {
    }

    public async Task<ParkingSession?> GetActiveByVehicleIdAsync(Guid vehicleId)
    {
        return await _context.ParkingSessions
            .FirstOrDefaultAsync(s =>
                s.VehicleId == vehicleId &&
                s.ExitTime == null);
    }
}
