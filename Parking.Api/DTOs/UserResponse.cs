namespace Parking.Api.DTOs;

public record UserResponse(
    Guid Id,
    string Name,
    string Email,
    string Role,
    bool IsActive
);
