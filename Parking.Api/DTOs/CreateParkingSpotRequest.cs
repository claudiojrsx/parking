using Parking.Domain.Enums;

namespace Parking.Api.DTOs
{
    public class CreateParkingSpotRequest
    {
        public string Code { get; set; } = null!;
        public ParkingSpotType Type { get; set; }
    }
}
