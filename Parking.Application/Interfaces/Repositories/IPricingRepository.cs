using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Repositories
{
    public interface IPricingRepository
    {
        Task<decimal> CalculateAsync(VehicleType type, DateTime entry, DateTime exit);
        Task<HourlyRate> GetCurrentRateAsync();
    }
}
