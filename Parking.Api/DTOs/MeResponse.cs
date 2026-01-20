namespace Parking.Api.DTOs;

public record MeResponse(
    Guid Id,
    string Email,
    string Role
);
