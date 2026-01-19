namespace Parking.Api.DTOs
{
    public record CreatePricingRequest(
        decimal Motorcycle,
        decimal Car,
        decimal Truck
    );
}
