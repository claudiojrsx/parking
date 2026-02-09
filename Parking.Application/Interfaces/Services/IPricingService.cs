using Parking.Domain.Enums;

namespace Parking.Application.Interfaces.Services
{
    public interface IPricingService
    {
        decimal Calculate(
            VehicleType vehicleType,
            DateTime entryTime,
            DateTime exitTime
        );
    }
}
