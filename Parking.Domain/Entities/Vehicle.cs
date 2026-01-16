using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Domain.Entities;

public class Vehicle(LicensePlate licensePlate, VehicleType type)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public LicensePlate LicensePlate { get; private set; } = licensePlate;
    public VehicleType Type { get; private set; } = type;
}
