using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSessionRepository : IRepository<ParkingSession>
{
    Task<ParkingSession?> GetActiveSessionByPlateAsync(string plate);
    Task<IEnumerable<ParkingSession>> GetByPeriodAsync(DateTime start, DateTime end);
    Task<ParkingSession?> GetActiveSessionByVehicleIdAsync(Guid vehicleId);
}
