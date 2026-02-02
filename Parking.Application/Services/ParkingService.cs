using Microsoft.EntityFrameworkCore;
using Parking.Application.DTOs;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Services;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}

public class ParkingService(
    IVehicleRepository vehicleRepo,
    IParkingSpotRepository spotRepo,
    IParkingSessionRepository sessionRepo,
    IPricingRepository pricingRepo)
{
    private readonly IVehicleRepository _vehicleRepo = vehicleRepo;
    private readonly IParkingSpotRepository _spotRepo = spotRepo;
    private readonly IParkingSessionRepository _sessionRepo = sessionRepo;
    private readonly IPricingRepository _pricingRepo = pricingRepo;

    public async Task<Guid> CheckInAsync(string plate, VehicleType type)
    {
        if (string.IsNullOrWhiteSpace(plate))
            throw new BusinessException("Placa inválida.");

        var plateNormalized = plate.Trim().ToUpper();
        var licensePlate = new LicensePlate(plateNormalized);

        // Buscando o veículo pela placa
        var vehicle = await _vehicleRepo.GetByLicensePlateAsync(licensePlate);

        if (vehicle is not null)
        {
            // Verifica se já existe sessão ativa para esse veículo
            var activeSession = await _sessionRepo.GetActiveByVehicleIdAsync(vehicle.Id);
            if (activeSession is not null)
            {
                throw new BusinessException(
                    "Esta placa já possui um check-in ativo para um tipo de veículo."
                );
            }
        }
        else
        {
            // Cria o veículo se não existir
            vehicle = new Vehicle(licensePlate, type);
            await _vehicleRepo.AddAsync(vehicle);
        }

        // Busca vaga disponível
        var spot = await _spotRepo.GetAvailableAsync((ParkingSpotType)type)
            ?? throw new InvalidOperationException("Não há vagas disponíveis para este tipo de veículo.");

        spot.Occupy();

        // Cria sessão
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

    public async Task<IEnumerable<ActiveParkingSessionDto>> GetActiveSessionsAsync()
    {
        var sessions = await _sessionRepo.GetActiveAsync();

        var result = new List<ActiveParkingSessionDto>();

        foreach (var session in sessions)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(session.VehicleId)
                ?? throw new InvalidOperationException("Vehicle not found");

            result.Add(new ActiveParkingSessionDto
            {
                SessionId = session.Id,
                VehicleId = vehicle.Id,
                Plate = vehicle.LicensePlate.Value,
                VehicleType = vehicle.Type,
                CheckInAt = session.EntryTime
            });
        }

        return result;
    }
}
