using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Domain.Services
{
    public class ParkingFeeCalculator
    {
        public decimal Calculate(
            VehicleType vehicleType,
            TimeSpan duration,
            HourlyRate rate)
        {
            var hours = Math.Ceiling(duration.TotalHours);

            return vehicleType switch
            {
                VehicleType.Motorcycle => (decimal)hours * rate.Motorcycle,
                VehicleType.Car => (decimal)hours * rate.Car,
                VehicleType.Truck => (decimal)hours * rate.Truck,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
