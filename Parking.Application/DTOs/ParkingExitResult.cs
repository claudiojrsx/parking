namespace Parking.Application.DTOs;

public record ParkingExitResult(
    Guid UsageId,
    decimal TotalAmount,
    DateTime EntryTime,
    DateTime ExitTime
);
