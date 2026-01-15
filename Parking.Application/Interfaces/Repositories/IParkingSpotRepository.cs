using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSpotRepository
{
    Task<ParkingSpot?> GetAvailableAsync();
    Task UpdateAsync(ParkingSpot parkingSpot);
}
