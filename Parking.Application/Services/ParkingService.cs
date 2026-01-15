using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;

namespace Parking.Application.Services;

public class ParkingService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IParkingSpotRepository _spotRepository;
    private readonly IParkingSessionRepository _sessionRepository;

    public ParkingService(
        IVehicleRepository vehicleRepository,
        IParkingSpotRepository spotRepository,
        IParkingSessionRepository sessionRepository)
    {
        _vehicleRepository = vehicleRepository;
        _spotRepository = spotRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<Guid> StartParkingAsync(Guid vehicleId)
    {
        var spot = await _spotRepository.GetAvailableAsync()
            ?? throw new InvalidOperationException("No available parking spots");

        spot.Occupy();

        var session = new ParkingSession(vehicleId, spot.Id);

        await _spotRepository.UpdateAsync(spot);
        await _sessionRepository.AddAsync(session);

        return session.Id;
    }

    public async Task EndParkingAsync(Guid vehicleId)
    {
        var session = await _sessionRepository.GetActiveByVehicleIdAsync(vehicleId)
            ?? throw new InvalidOperationException("Active parking session not found");

        session.CloseSession();

        await _sessionRepository.UpdateAsync(session);
    }
}
