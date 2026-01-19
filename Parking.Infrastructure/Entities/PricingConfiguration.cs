namespace Parking.Infrastructure.Entities;

public class PricingConfiguration
{
    public Guid Id { get; set; }

    public decimal MotorcycleHourlyRate { get; set; }
    public decimal CarHourlyRate { get; set; }
    public decimal TruckHourlyRate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
