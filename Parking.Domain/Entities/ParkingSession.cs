using Parking.Domain.Enums;
using Parking.Domain.Services;
using Parking.Domain.ValueObjects;

namespace Parking.Domain.Entities;

public class ParkingSession
{
    public Guid Id { get; private set; }
    public Guid VehicleId { get; private set; }
    public Guid ParkingSpotId { get; private set; }
    public DateTime EntryTime { get; private set; }
    public DateTime? ExitTime { get; private set; }

    protected ParkingSession() { }

    public ParkingSession(Guid vehicleId, Guid parkingSpotId)
    {
        Id = Guid.NewGuid();
        VehicleId = vehicleId;
        ParkingSpotId = parkingSpotId;
        EntryTime = DateTime.UtcNow;
    }

    public void CloseSession()
    {
        if (ExitTime != null)
            throw new InvalidOperationException("Session already closed");

        ExitTime = DateTime.UtcNow;
    }

    public TimeSpan GetDuration()
    {
        return (ExitTime ?? DateTime.UtcNow) - EntryTime;
    }

    public decimal CalculateFee(
    VehicleType vehicleType,
    HourlyRate rate)
    {
        if (ExitTime == null)
            throw new InvalidOperationException("Session must be closed before calculating fee");

        var calculator = new ParkingFeeCalculator();
        return calculator.Calculate(vehicleType, GetDuration(), rate);
    }
}
