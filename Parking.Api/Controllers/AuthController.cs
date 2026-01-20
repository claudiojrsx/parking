using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Auth;
using Parking.Api.DTOs;
using Parking.Application.Services;
using System.Security.Claims;

namespace Parking.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(
            request.Email,
            request.Password);

        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("claims")]
    public IActionResult Claims()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Email);
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (userId == null || email == null || role == null)
            return Unauthorized();

        var response = new MeResponse(
            Guid.Parse(userId),
            email,
            role
        );

        return Ok(response);
    }
}
