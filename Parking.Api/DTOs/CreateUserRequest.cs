namespace Parking.Api.DTOs
{
    public record CreateUserRequest(
        string Name,
        string Email,
        string Password,
        Guid RoleId
    );
}
