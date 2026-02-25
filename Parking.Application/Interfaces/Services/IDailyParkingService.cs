using Parking.Application.DTOs;
using Parking.Domain.Entities;
using Parking.Domain.Enums;

namespace Parking.Application.Interfaces.Services;

public interface IDailyParkingService
{
    Task<ParkingUsage> RegisterEntryAsync(string plate, VehicleType type);
    Task<ParkingUsage> RegisterEntryAsync(Guid vehicleId);
    Task<ParkingExitResult> RegisterExitAsync(Guid usageId);
    Task<List<ActiveParkingSessionDto>> GetActiveAsync();
}
