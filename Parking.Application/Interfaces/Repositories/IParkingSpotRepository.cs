using Parking.Domain.Entities;
using Parking.Domain.Enums;
using Parking.Application.DTOs;

namespace Parking.Application.Interfaces.Repositories;

public interface IParkingSpotRepository : IRepository<ParkingSpot>
{
    Task<ParkingSpot?> GetAvailableAsync(ParkingSpotType type);
    Task<List<ParkingSpot>> GetAvailableByTypeAsync(ParkingSpotType type);
    Task<List<ParkingSpot>> GetAllByTypeAsync(ParkingSpotType type);
    Task<ParkingSpotSummary> GetSummaryAsync(ParkingSpotType type);
}
