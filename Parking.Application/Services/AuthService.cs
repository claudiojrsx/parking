using Parking.Application.Interfaces.Repositories;
using Parking.Application.Interfaces.Security;

namespace Parking.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(
        IUserRepository userRepo,
        IJwtTokenGenerator jwt)
    {
        _userRepo = userRepo;
        _jwt = jwt;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepo.GetByEmailAsync(email) ?? throw new Exception("Role não carregada");
        var isValid = BCrypt.Net.BCrypt.Verify(
            password,
            user.PasswordHash);

        if (!isValid)
            return null;

        return _jwt.GenerateToken(user);
    }
}
