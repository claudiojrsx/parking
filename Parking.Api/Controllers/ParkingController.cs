using Microsoft.AspNetCore.Mvc;
using Parking.Api.DTOs;
using Parking.Application.Services;

namespace Parking.Api.Controllers;

[ApiController]
[Route("api/parking")]
public class ParkingController : ControllerBase
{
    private readonly ParkingService _parkingService;

    public ParkingController(ParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInRequest request)
    {
        var sessionId = await _parkingService.CheckInAsync(
            request.Plate,
            request.VehicleType);

        return Ok(new CheckInResponse
        {
            SessionId = sessionId
        });
    }

    [HttpPost("check-out/{vehicleId:guid}")]
    public async Task<IActionResult> CheckOut(Guid vehicleId)
    {
        await _parkingService.CheckOutAsync(vehicleId);
        return NoContent();
    }
}
