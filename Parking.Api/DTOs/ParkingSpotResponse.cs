namespace Parking.Api.DTOs
{
    public record ParkingSpotResponse(
        Guid Id,
        string Code,
        string Type,
        bool IsOccupied
    );
}
