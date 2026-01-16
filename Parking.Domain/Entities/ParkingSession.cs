namespace Parking.Domain.Entities;

public class ParkingSession
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid ParkingSpotId { get; private set; }
    public DateTime EntryTime { get; private set; }
    public DateTime? ExitTime { get; private set; }

    protected ParkingSession() { }

    public ParkingSession(Guid vehicleId, Guid spotId)
    {
        Id = Guid.NewGuid();
        VehicleId = vehicleId;
        ParkingSpotId = spotId;
        EntryTime = DateTime.UtcNow;
    }

    public void Close()
    {
        if (ExitTime != null)
            throw new InvalidOperationException("Session already closed");

        ExitTime = DateTime.UtcNow;
    }
}
