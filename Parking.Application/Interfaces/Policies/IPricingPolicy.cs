using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Policies;

public interface IPricingPolicy
{
    HourlyRate GetCurrentRate();
}
