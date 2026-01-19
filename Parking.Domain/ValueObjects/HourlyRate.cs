namespace Parking.Domain.ValueObjects
{
    public class HourlyRate
    {
        public decimal Motorcycle { get; }
        public decimal Car { get; }
        public decimal Truck { get; }

        public HourlyRate(decimal motorcycle, decimal car, decimal truck)
        {
            Motorcycle = motorcycle;
            Car = car;
            Truck = truck;
        }
    }
}
