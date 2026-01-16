using Parking.Domain.Entities;
using Parking.Domain.Enums;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSpotRepository
{
    Task<ParkingSpot?> GetAvailableAsync();
    Task UpdateAsync(ParkingSpot parkingSpot);
    Task<ParkingSpot?> GetAvailableSpotAsync(VehicleType type);

}
