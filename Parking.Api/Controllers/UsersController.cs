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
    public class UsersController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var userId = await _userService.CreateUserAsync(
                request.Name,
                request.Email,
                request.Password,
                request.RoleId,
                request.IsActive);

            return CreatedAtAction(nameof(Create), new { userId });
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
        {
            var users = await _userService.GetAllAsync();

            var response = users.Select(u => new UserResponse(
                u.Id,
                u.Name,
                u.Email,
                u.Role.Name,
                u.IsActive
            ));

            return Ok(response);
        }
    }
}
