using Parking.Application.Interfaces.Policies;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Policies;

public class FixedPricingPolicy : IPricingPolicy
{
    public HourlyRate GetCurrentRate()
    {
        return new HourlyRate(5, 10, 20);
    }
}
