using Parking.Domain.Enums;

namespace Parking.Domain.Entities;

public class ParkingSpot
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public ParkingSpotType Type { get; set; }
    public bool IsOccupied { get; set; }

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
            throw new InvalidOperationException("Spot already occupied");

        IsOccupied = true;
    }

    public void Release()
    {
        IsOccupied = false;
    }
}
