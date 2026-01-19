using Parking.Application.DTOs;
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
    private readonly IPricingRepository _pricingRepo;

    public ParkingService(
        IVehicleRepository vehicleRepo,
        IParkingSpotRepository spotRepo,
        IParkingSessionRepository sessionRepo,
        IPricingRepository pricingRepo)
    {
        _vehicleRepo = vehicleRepo;
        _spotRepo = spotRepo;
        _sessionRepo = sessionRepo;
        _pricingRepo = pricingRepo;
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

    public async Task<CheckOutResult> CheckOutAsync(Guid vehicleId)
    {
        var session = await _sessionRepo.GetActiveByVehicleIdAsync(vehicleId)
            ?? throw new InvalidOperationException("Active session not found");

        session.Close();

        var spot = await _spotRepo.GetByIdAsync(session.ParkingSpotId)
            ?? throw new InvalidOperationException("Parking spot not found");

        spot.Release();

        var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId)
            ?? throw new InvalidOperationException("Vehicle not found");

        var hourlyRate = await _pricingRepo.GetCurrentRateAsync();

        var duration = session.GetDuration();
        var amount = session.CalculateFee(vehicle.Type, hourlyRate);

        await _sessionRepo.UpdateAsync(session);
        await _spotRepo.UpdateAsync(spot);

        return new CheckOutResult
        {
            SessionId = session.Id,
            VehicleId = vehicleId,
            Duration = duration,
            Amount = amount
        };
    }
}
