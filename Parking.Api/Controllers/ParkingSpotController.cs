using Microsoft.AspNetCore.Mvc;
using Parking.Api.DTOs;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;

[ApiController]
[Route("api/parking-spots")]
public class ParkingSpotController : ControllerBase
{
    private readonly IParkingSpotRepository _spotRepo;

    public ParkingSpotController(IParkingSpotRepository spotRepo)
    {
        _spotRepo = spotRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateParkingSpotRequest request)
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
    public async Task<IActionResult> GetAvailable()
    {
        var spots = await _spotRepo.GetAllAsync();
        return Ok(spots.Where(s => !s.IsOccupied));
    }
}
