using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Repositories
{
    public interface IPricingRepository
    {
        decimal Calculate(VehicleType type, DateTime value1, DateTime value2);
        Task<HourlyRate> GetCurrentRateAsync();
    }
}
