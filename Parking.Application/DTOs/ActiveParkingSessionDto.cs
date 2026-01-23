using Parking.Domain.Enums;

namespace Parking.Application.DTOs;

public class ActiveParkingSessionDto
{
    public Guid SessionId { get; init; }
    public Guid VehicleId { get; init; }
    public string Plate { get; init; } = default!;
    public VehicleType VehicleType { get; init; }
    public DateTime CheckInAt { get; init; }
}
