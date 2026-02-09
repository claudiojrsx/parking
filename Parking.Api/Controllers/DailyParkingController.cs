using Microsoft.AspNetCore.Mvc;
using Parking.Application.Services;

namespace Parking.Api.Controllers;

[ApiController]
[Route("api/parking/daily")]
public class DailyParkingController : ControllerBase
{
    private readonly DailyParkingService _service;

    public DailyParkingController(DailyParkingService service)
    {
        _service = service;
    }

    [HttpPost("entry/{vehicleId}")]
    public async Task<IActionResult> Entry(Guid vehicleId)
    {
        var usage = await _service.RegisterEntryAsync(vehicleId);
        return Ok(usage);
    }

    [HttpPost("exit/{usageId}")]
    public async Task<IActionResult> Exit(Guid usageId)
    {
        var result = await _service.RegisterExitAsync(usageId);
        return Ok(result);
    }
}
