using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Auth;
using System.Security.Claims;

namespace Parking.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("auth")]
        public IActionResult AuthTest()
        {
            return Ok(new
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Role = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("Você é Admin 😎");
        }
    }
}