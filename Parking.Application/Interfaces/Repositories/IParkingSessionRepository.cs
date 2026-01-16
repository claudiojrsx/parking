using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSessionRepository : IRepository<ParkingSession>
{
    Task<ParkingSession?> GetActiveByVehicleIdAsync(Guid vehicleId);
}
