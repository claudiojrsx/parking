namespace Parking.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Parking.Api.Auth;
    using Parking.Api.DTOs;
    using Parking.Application.Services;

    [Authorize]
    [ApiController]
    [Route("api/parking")]
    public class ParkingController : ControllerBase
    {
        private readonly ParkingService _parkingService;

        public ParkingController(ParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
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

        [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut(Guid vehicleId)
        {
            var result = await _parkingService.CheckOutAsync(vehicleId);
            return Ok(result);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
        [HttpGet("sessions/active")]
        public async Task<IActionResult> GetActiveSessions()
        {
            var sessions = await _parkingService.GetActiveSessionsAsync();
            return Ok(sessions);
        }
    }
}