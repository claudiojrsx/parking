namespace Parking.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Parking.Api.Auth;
    using Parking.Api.DTOs;
    using Parking.Application.Interfaces.Repositories;
    using Parking.Domain.Entities;
    using Parking.Domain.Enums;

    [ApiController]
    [Route("api/parking-spots")]
    [Authorize(Roles = Roles.Admin)]
    public class ParkingSpotController(IParkingSpotRepository spotRepo) : ControllerBase
    {
        private readonly IParkingSpotRepository _spotRepo = spotRepo;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParkingSpotRequest request)
        {
            var spot = new ParkingSpot(request.Code, request.Type);
            await _spotRepo.AddAsync(spot);

            var response = new ParkingSpotResponse(
                spot.Id,
                spot.Code,
                spot.Type.ToString(),
                spot.IsOccupied
            );

            return CreatedAtAction(nameof(Create), response);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(ParkingSpotType type)
        {
            var spot = await _spotRepo.GetAvailableAsync(type);

            if (spot == null)
                return NotFound();

            return Ok(new ParkingSpotResponse(
                spot.Id,
                spot.Code,
                spot.Type.ToString(),
                spot.IsOccupied
            ));
        }

        [HttpPatch("{spotId:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var spot = await _spotRepo.GetByIdAsync(id);
            if (spot == null)
                return NotFound();

            spot.Deactivate();
            await _spotRepo.UpdateAsync(spot);

            return NoContent();
        }
    }
}
