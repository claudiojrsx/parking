using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Services;

public interface IPricingPolicy
{
    HourlyRate GetCurrentRate();
}
