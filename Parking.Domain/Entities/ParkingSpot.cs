using Parking.Domain.Enums;

namespace Parking.Domain.Entities;

public class ParkingSpot
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public ParkingSpotType Type { get; set; }
    public bool IsOccupied { get; set; }
    public bool IsActive { get; private set; }

    protected ParkingSpot() { }

    public ParkingSpot(string code, ParkingSpotType type)
    {
        Id = Guid.NewGuid();
        Code = code;
        Type = type;
        IsOccupied = false;
        IsActive = true;
    }

    public void Occupy()
    {
        if (!IsActive)
            throw new InvalidOperationException("Parking spot is inactive");

        if (IsOccupied)
            throw new InvalidOperationException("Parking spot is already occupied");

        IsOccupied = true;
    }

    public void Release()
    {
        IsOccupied = false;
    }

    public void Deactivate()
    {
        if (IsOccupied)
            throw new InvalidOperationException("Cannot deactivate an occupied spot");

        IsActive = false;
    }
}
