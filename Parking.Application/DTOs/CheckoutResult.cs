namespace Parking.Application.DTOs
{
    public class CheckOutResult
    {
        public Guid SessionId { get; init; }
        public Guid VehicleId { get; init; }
        public TimeSpan Duration { get; init; }
        public decimal Amount { get; init; }
    }
}
