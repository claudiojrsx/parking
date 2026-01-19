using Microsoft.AspNetCore.Identity;
using Parking.Application.Interfaces.Repositories;
using Parking.Application.Interfaces.Security;
using Parking.Domain.Entities;

namespace Parking.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IJwtTokenGenerator _jwt;
    private readonly PasswordHasher<User> _hasher = new();

    public AuthService(
        IUserRepository userRepo,
        IJwtTokenGenerator jwt)
    {
        _userRepo = userRepo;
        _jwt = jwt;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepo.GetByEmailAsync(email);
        if (user == null)
            return null;

        var isValid = BCrypt.Net.BCrypt.Verify(
            password,
            user.PasswordHash);

        if (!isValid)
            return null;

        return _jwt.GenerateToken(user);
    }
}
