using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSessionRepository
{
    Task AddAsync(ParkingSession session);
    Task<ParkingSession?> GetActiveByVehicleIdAsync(Guid vehicleId);
    Task UpdateAsync(ParkingSession session);
}
