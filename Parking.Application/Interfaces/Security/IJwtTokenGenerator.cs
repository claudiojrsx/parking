using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
