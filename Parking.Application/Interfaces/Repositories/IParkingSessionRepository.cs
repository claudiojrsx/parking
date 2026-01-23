using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSessionRepository : IRepository<ParkingSession>
{
    Task<IEnumerable<ParkingSession>> GetActiveAsync();
    Task<ParkingSession?> GetActiveByVehicleIdAsync(Guid vehicleId);
}
