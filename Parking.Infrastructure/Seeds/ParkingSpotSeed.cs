using Parking.Domain.Entities;
using Parking.Domain.Enums;

namespace Parking.Infrastructure.Seeds;

public static class ParkingSpotSeed
{
    public static List<ParkingSpot> Get()
    {
        return new List<ParkingSpot>
        {
            new ParkingSpot("M-01", ParkingSpotType.Motorcycle)
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111")
            },
            new ParkingSpot("M-02", ParkingSpotType.Motorcycle)
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111112")
            },

            new ParkingSpot("C-01", ParkingSpotType.Car)
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222221")
            },
            new ParkingSpot("C-02", ParkingSpotType.Car)
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222")
            },

            new ParkingSpot("T-01", ParkingSpotType.Truck)
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333331")
            }
        };
    }
}
