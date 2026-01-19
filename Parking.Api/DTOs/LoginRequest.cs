using System.ComponentModel.DataAnnotations;

namespace Parking.Api.DTOs;

public record LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; init; } = string.Empty;
}
