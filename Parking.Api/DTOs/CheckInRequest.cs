using Parking.Domain.Enums;

namespace Parking.Api.DTOs;

public class CheckInRequest
{
    public string Plate { get; set; } = string.Empty;
    public VehicleType VehicleType { get; set; }
}
