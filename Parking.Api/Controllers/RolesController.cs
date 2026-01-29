using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Auth;
using Parking.Api.DTOs;
using Parking.Application.Services;

namespace Parking.Api.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize(Roles = Roles.Admin)]
public class RolesController : ControllerBase
{
    private readonly RoleService _roleService;

    public RolesController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAll()
    {
        var roles = await _roleService.GetAllAsync();

        var response = roles.Select(r => new RoleResponse(
            r.Id,
            r.Name
        ));

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
        try
        {
            var roleId = await _roleService.CreateAsync(request.Name);
            return CreatedAtAction(nameof(GetAll), new { roleId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
