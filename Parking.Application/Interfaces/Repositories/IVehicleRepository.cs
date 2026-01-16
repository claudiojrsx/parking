using Parking.Domain.Entities;
using Parking.Domain.ValueObjects;

namespace Parking.Application.Interfaces.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle);
    Task<Vehicle?> GetByIdAsync(Guid id);
    Task<Vehicle?> GetByLicensePlateAsync(LicensePlate plate);

}
