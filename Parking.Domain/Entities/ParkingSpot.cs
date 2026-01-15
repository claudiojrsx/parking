using Parking.Domain.Enums;

namespace Parking.Domain.Entities;

public class ParkingSpot
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public ParkingSpotType Type { get; private set; }
    public bool IsOccupied { get; private set; }

    protected ParkingSpot() { }

    public ParkingSpot(string code, ParkingSpotType type)
    {
        Id = Guid.NewGuid();
        Code = code;
        Type = type;
        IsOccupied = false;
    }

    public void Occupy()
    {
        if (IsOccupied)
            throw new InvalidOperationException("Parking spot is already occupied");

        IsOccupied = true;
    }

    public void Release()
    {
        IsOccupied = false;
    }
}
