using Parking.Domain.Entities;

namespace Parking.Application.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    new Task<IEnumerable<Role>> GetAllAsync();
    new Task AddAsync(Role role);
}
