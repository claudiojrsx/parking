using Microsoft.AspNetCore.Mvc;
using Parking.Api.DTOs;
using Parking.Infrastructure.Context;
using Parking.Infrastructure.Entities;

namespace Parking.Api.Controllers
{
    [ApiController]
    [Route("api/pricing")]
    public class PricingController : ControllerBase
    {
        private readonly ParkingDbContext _context;

        public PricingController(ParkingDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePricingRequest request)
        {
            // Desativa preços antigos
            var actives = _context.PricingConfigurations
                .Where(p => p.IsActive);

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
    }

}
