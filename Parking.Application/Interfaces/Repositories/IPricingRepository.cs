using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Repositories
{
    public interface IPricingRepository
    {
        Task<HourlyRate> GetCurrentRateAsync();
    }
}
