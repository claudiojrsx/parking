namespace Parking.Domain.ValueObjects;

public class HourlyRate
{
    public decimal Motorcycle { get; }
    public decimal Car { get; }
    public decimal Truck { get; }

    public HourlyRate(decimal motorcycle, decimal car, decimal truck)
    {
        if (motorcycle <= 0 || car <= 0 || truck <= 0)
            throw new ArgumentException("Hourly rates must be greater than zero");

        Motorcycle = motorcycle;
        Car = car;
        Truck = truck;
    }
}
