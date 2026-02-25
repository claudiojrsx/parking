using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories
{
    public interface IParkingUsageRepository
    {
        Task<ParkingUsage?> GetActiveByVehicleAsync(Guid vehicleId);
        Task<ParkingUsage?> GetActiveBySpotAsync(Guid parkingSpotId);
        Task<ParkingUsage?> GetByIdAsync(Guid id);
        Task<List<ParkingUsage>> GetAllActiveAsync();
        Task AddAsync(ParkingUsage usage);
        Task UpdateAsync(ParkingUsage usage);
    }
}
