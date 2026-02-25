using Parking.Application.DTOs;
using Parking.Application.Interfaces.Repositories;
using Parking.Application.Interfaces.Services;
using Parking.Domain.Entities;
using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Services;

public static class VehicleTypeExtensions
{
    public static ParkingSpotType ToParkingSpotType(this VehicleType type)
    {
        return type switch
        {
            VehicleType.Motorcycle => ParkingSpotType.Motorcycle,
            VehicleType.Car => ParkingSpotType.Car,
            VehicleType.Truck => ParkingSpotType.Truck,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}

public class DailyParkingService(
    IParkingUsageRepository usageRepo,
    IParkingSpotRepository spotRepo,
    IVehicleRepository vehicleRepo,
    IPricingService pricingService) : IDailyParkingService
{
    private readonly IParkingUsageRepository _usageRepo = usageRepo;
    private readonly IParkingSpotRepository _spotRepo = spotRepo;
    private readonly IVehicleRepository _vehicleRepo = vehicleRepo;
    private readonly IPricingService _pricingService = pricingService;

    public async Task<ParkingUsage> RegisterEntryAsync(string plate, VehicleType type)
    {
        if (string.IsNullOrWhiteSpace(plate))
            throw new InvalidOperationException("Placa inválida.");

        var plateNormalized = plate.Trim().ToUpperInvariant();
        var licensePlate = new LicensePlate(plateNormalized);

        var vehicle = await _vehicleRepo.GetByLicensePlateAsync(licensePlate);

        if (vehicle is null)
        {
            vehicle = new Vehicle(licensePlate, type);
            await _vehicleRepo.AddAsync(vehicle);
        }

        return await RegisterEntryAsync(vehicle.Id);
    }

    public async Task<ParkingUsage> RegisterEntryAsync(Guid vehicleId)
    {
        var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId)
            ?? throw new InvalidOperationException("Veículo não encontrado.");

        var activeUsage = await _usageRepo.GetActiveByVehicleAsync(vehicleId);
        if (activeUsage != null)
            throw new InvalidOperationException("Veículo já possui uma vaga ativa.");

        var spot = await _spotRepo.GetAvailableAsync(vehicle.Type.ToParkingSpotType())
            ?? throw new InvalidOperationException("Nenhuma vaga disponível para este tipo de veículo.");

        spot.Occupy();

        var usage = new ParkingUsage(
            ParkingUsageType.Daily,
            spot.Id,
            vehicle.Id,
            entryTime: DateTime.UtcNow
        );

        await _usageRepo.AddAsync(usage);
        await _spotRepo.UpdateAsync(spot);

        return usage;
    }

    public async Task<ParkingExitResult> RegisterExitAsync(Guid usageId)
    {
        var usage = await _usageRepo.GetByIdAsync(usageId)
            ?? throw new InvalidOperationException("Uso da vaga não encontrado.");

        if (!usage.IsActive)
            throw new InvalidOperationException("Esta vaga já foi finalizada.");

        usage.RegisterExit(DateTime.UtcNow);

        var total = _pricingService.Calculate(
            usage.Vehicle.Type,
            usage.EntryTime!.Value,
            usage.ExitTime!.Value
        );

        usage.ParkingSpot.Release();

        await _usageRepo.UpdateAsync(usage);
        await _spotRepo.UpdateAsync(usage.ParkingSpot);

        return new ParkingExitResult(
            usage.Id,
            total,
            usage.EntryTime.Value,
            usage.ExitTime.Value
        );
    }

    public async Task<List<ActiveParkingSessionDto>> GetActiveAsync()
    {
        var usages = await _usageRepo.GetAllActiveAsync();

        return [.. usages.Select(u => new ActiveParkingSessionDto
        {
            SessionId = u.Id,
            VehicleId = u.Vehicle.Id,
            Plate = u.Vehicle.LicensePlate.Value,
            VehicleType = u.Vehicle.Type,
            CheckInAt = u.EntryTime!.Value
        })];
    }
}
