using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Services;

public class ParkingService
{
    private readonly IVehicleRepository _vehicleRepo;
    private readonly IParkingSpotRepository _spotRepo;
    private readonly IParkingSessionRepository _sessionRepo;

    public ParkingService(
        IVehicleRepository vehicleRepo,
        IParkingSpotRepository spotRepo,
        IParkingSessionRepository sessionRepo)
    {
        _vehicleRepo = vehicleRepo;
        _spotRepo = spotRepo;
        _sessionRepo = sessionRepo;
    }

    public async Task<Guid> CheckInAsync(string plate, VehicleType type)
    {
        var licensePlate = new LicensePlate(plate);

        var vehicle = await _vehicleRepo.GetByLicensePlateAsync(licensePlate)
            ?? new Vehicle(licensePlate, type);

        if (vehicle.Id == Guid.Empty)
            await _vehicleRepo.AddAsync(vehicle);

        var spot = await _spotRepo.GetAvailableAsync((ParkingSpotType)type)
            ?? throw new InvalidOperationException("No available spot");

        spot.Occupy();

        var session = new ParkingSession(vehicle.Id, spot.Id);

        await _spotRepo.UpdateAsync(spot);
        await _sessionRepo.AddAsync(session);

        return session.Id;
    }

    public async Task CheckOutAsync(Guid vehicleId)
    {
        var session = await _sessionRepo.GetActiveByVehicleIdAsync(vehicleId)
            ?? throw new InvalidOperationException("Active session not found");

        session.Close();
        await _sessionRepo.UpdateAsync(session);
    }
}
