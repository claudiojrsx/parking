using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking.Api.Auth;
using Parking.Api.DTOs;
using Parking.Application.Interfaces.Services;

namespace Parking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/parking/daily")]
public class DailyParkingController : ControllerBase
{
    private readonly IDailyParkingService _service;

    public DailyParkingController(IDailyParkingService service)
    {
        _service = service;
    }

    [Authorize(Roles = $"{Roles.Admin}")]
    [HttpPost("entry")]
    public async Task<IActionResult> EntryByPlate([FromBody] CheckInRequest request)
    {
        var usage = await _service.RegisterEntryAsync(request.Plate, request.VehicleType);
        return Ok(usage);
    }

    [Authorize(Roles = $"{Roles.Admin}")]
    [HttpPost("entry/{vehicleId}")]
    public async Task<IActionResult> Entry(Guid vehicleId)
    {
        var usage = await _service.RegisterEntryAsync(vehicleId);
        return Ok(usage);
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
    [HttpPost("exit/{usageId}")]
    public async Task<IActionResult> Exit(Guid usageId)
    {
        var result = await _service.RegisterExitAsync(usageId);
        return Ok(result);
    }

    [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var active = await _service.GetActiveAsync();
        return Ok(active);
    }
}
