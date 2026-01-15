using Parking.Domain.Enums;
using Parking.Domain.ValueObjects;

namespace Parking.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public VehicleType Type { get; private set; }

    protected Vehicle() { }

    public Vehicle(LicensePlate licensePlate, VehicleType type)
    {
        Id = Guid.NewGuid();
        LicensePlate = licensePlate;
        Type = type;
    }
}
