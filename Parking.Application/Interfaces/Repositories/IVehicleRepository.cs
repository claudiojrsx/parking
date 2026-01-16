using Parking.Domain.Entities;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Repositories;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<Vehicle?> GetByLicensePlateAsync(LicensePlate plate);
}
