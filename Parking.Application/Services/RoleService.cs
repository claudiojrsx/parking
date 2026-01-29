using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;

namespace Parking.Application.Services;

public class RoleService(IRoleRepository roleRepository)
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task<Guid> CreateAsync(string name)
    {
        var existing = await _roleRepository
            .GetAllAsync();

        if (existing.Any(r => r.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            throw new InvalidOperationException("Role já existe");

        var role = new Role(name);
        await _roleRepository.AddAsync(role);

        return role.Id;
    }
}
