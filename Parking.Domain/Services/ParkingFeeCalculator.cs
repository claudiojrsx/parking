using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Domain.Services;

public class ParkingFeeCalculator
{
    public decimal Calculate(
        VehicleType vehicleType,
        TimeSpan duration,
        HourlyRate rate)
    {
        decimal totalHours = (decimal)Math.Ceiling(duration.TotalHours);

        return vehicleType switch
        {
            VehicleType.Motorcycle => totalHours * rate.Motorcycle,
            VehicleType.Car => totalHours * rate.Car,
            VehicleType.Truck => totalHours * rate.Truck,
            _ => throw new ArgumentOutOfRangeException(nameof(vehicleType))
        };
    }
}
