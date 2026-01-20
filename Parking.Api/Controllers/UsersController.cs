using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Auth;
using Parking.Api.DTOs;
using Parking.Application.Services;

namespace Parking.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var userId = await _userService.CreateUserAsync(
                request.Name,
                request.Email,
                request.Password,
                request.RoleId);

            return CreatedAtAction(nameof(Create), new { userId });
        }
    }
}
