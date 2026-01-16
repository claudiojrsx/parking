using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Domain.ValueObjects;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class VehicleRepository
    : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ParkingDbContext context)
        : base(context)
    {
    }

    public async Task<Vehicle?> GetByLicensePlateAsync(LicensePlate plate)
    {
        return await _context.Vehicles
            .FirstOrDefaultAsync(v => v.LicensePlate.Value == plate.Value);
    }
}
