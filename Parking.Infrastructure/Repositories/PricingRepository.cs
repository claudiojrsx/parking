using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.ValueObjects;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories
{
    public class PricingRepository : IPricingRepository
    {
        private readonly ParkingDbContext _context;

        public PricingRepository(ParkingDbContext context)
        {
            _context = context;
        }

        public async Task<HourlyRate> GetCurrentRateAsync()
        {
            var pricing = await _context.PricingConfigurations
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IsActive);

            if (pricing is null)
                throw new InvalidOperationException("Pricing configuration not found");

            return new HourlyRate(
                pricing.MotorcycleHourlyRate,
                pricing.CarHourlyRate,
                pricing.TruckHourlyRate
            );
        }
    }
}
