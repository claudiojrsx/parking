using Parking.Domain.Enums;

namespace Parking.Domain.Entities;

public class ParkingUsage
{
    protected ParkingUsage() { } // EF

    public ParkingUsage(
        ParkingUsageType usageType,
        Guid parkingSpotId,
        Guid vehicleId,
        DateTime? entryTime = null,
        DateTime? startDate = null,
        DateTime? endDate = null
    )
    {
        Id = Guid.NewGuid();
        UsageType = usageType;
        ParkingSpotId = parkingSpotId;
        VehicleId = vehicleId;

        EntryTime = entryTime;
        StartDate = startDate;
        EndDate = endDate;

        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public ParkingUsageType UsageType { get; private set; }

    public Guid ParkingSpotId { get; private set; }
    public ParkingSpot ParkingSpot { get; private set; } = null!;

    public Guid VehicleId { get; private set; }
    public Vehicle Vehicle { get; private set; } = null!;

    // Diário
    public DateTime? EntryTime { get; private set; }
    public DateTime? ExitTime { get; private set; }

    // Mensal
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    /* ======= Métodos de domínio ======= */

    public void RegisterExit(DateTime exitTime)
    {
        if (UsageType != ParkingUsageType.Daily)
            throw new InvalidOperationException("Saída permitida apenas para uso diário.");

        ExitTime = exitTime;
        IsActive = false;
    }

    public void FinishMonthlyContract()
    {
        if (UsageType != ParkingUsageType.Monthly)
            throw new InvalidOperationException("Apenas contratos mensais podem ser finalizados.");

        IsActive = false;
    }
}
