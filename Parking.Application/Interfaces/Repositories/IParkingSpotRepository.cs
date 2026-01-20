using Parking.Domain.Entities;
using Parking.Domain.Enums;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSpotRepository : IRepository<ParkingSpot>
{
    Task<ParkingSpot?> GetAvailableAsync(ParkingSpotType type);
    Task<List<ParkingSpot>> GetAvailableByTypeAsync(ParkingSpotType type);
}
