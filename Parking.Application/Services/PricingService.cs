using Parking.Application.Interfaces.Services;
using Parking.Domain.Enums;

namespace Parking.Application.Services;

public class PricingService : IPricingService
{
    public decimal Calculate(
        VehicleType vehicleType,
        DateTime entryTime,
        DateTime exitTime)
    {
        var totalHours = Math.Ceiling((exitTime - entryTime).TotalHours);

        decimal hourlyRate = vehicleType switch
        {
            VehicleType.Motorcycle => 5m,
            VehicleType.Car => 10m,
            VehicleType.Truck => 15m,
            _ => throw new ArgumentOutOfRangeException(nameof(vehicleType))
        };

        return hourlyRate * (decimal)totalHours;
    }
}
