using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle);
    Task<Vehicle?> GetByIdAsync(Guid id);
}
