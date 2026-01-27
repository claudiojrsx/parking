using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Auth;
using Parking.Api.DTOs;
using Parking.Infrastructure.Context;
using Parking.Infrastructure.Entities;

namespace Parking.Api.Controllers
{
    [Authorize(Roles = $"{Roles.Admin}")]
    [ApiController]
    [Route("api/pricing")]
    public class PricingController : ControllerBase
    {
        private readonly ParkingDbContext _context;

        public PricingController(ParkingDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePricingRequest request)
        {
            if (request.Motorcycle <= 0 || request.Car <= 0 || request.Truck <= 0)
                return BadRequest("Os valores devem ser maiores que zero.");

            var actives = await _context.PricingConfigurations
                .Where(p => p.IsActive)
                .ToListAsync();

            foreach (var p in actives)
                p.IsActive = false;

            var pricing = new PricingConfiguration
            {
                MotorcycleHourlyRate = request.Motorcycle,
                CarHourlyRate = request.Car,
                TruckHourlyRate = request.Truck,
                IsActive = true
            };

            _context.PricingConfigurations.Add(pricing);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Operator}")]
        [HttpGet("current")]
        public async Task<ActionResult<PricingResponse>> GetCurrent()
        {
            var pricing = await _context.PricingConfigurations
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IsActive);

            if (pricing is null)
                return NotFound();

            return Ok(new PricingResponse
            {
                Motorcycle = pricing.MotorcycleHourlyRate,
                Car = pricing.CarHourlyRate,
                Truck = pricing.TruckHourlyRate
            });
        }
    }
}
