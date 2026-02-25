using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class PricingRepository : IPricingRepository
{
    private readonly ParkingDbContext _context;

    public PricingRepository(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<HourlyRate> GetCurrentRateAsync()
    {
        var pricing = await _context.Pricings
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.IsActive);

        if (pricing is null)
            throw new InvalidOperationException("Active pricing not found");

        return new HourlyRate(
            pricing.MotorcycleHourlyRate,
            pricing.CarHourlyRate,
            pricing.TruckHourlyRate
        );
    }

    public async Task AddAsync(Pricing pricing)
    {
        _context.Pricings.Add(pricing);
        await _context.SaveChangesAsync();
    }

    public async Task<decimal> CalculateAsync(VehicleType type, DateTime entry, DateTime exit)
    {
        var pricing = await _context.Pricings
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.IsActive);

        if (pricing is null)
            throw new InvalidOperationException("Active pricing not found");

        var totalHours = Math.Ceiling((exit - entry).TotalHours);

        decimal hourlyRate = type switch
        {
            VehicleType.Motorcycle => pricing.MotorcycleHourlyRate,
            VehicleType.Car => pricing.CarHourlyRate,
            VehicleType.Truck => pricing.TruckHourlyRate,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        return (decimal)totalHours * hourlyRate;
    }
}
