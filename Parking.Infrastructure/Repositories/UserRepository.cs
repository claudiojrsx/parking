using Microsoft.EntityFrameworkCore;
using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;
using Parking.Infrastructure.Context;

namespace Parking.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ParkingDbContext _context;

    public UserRepository(ParkingDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Set<User>()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }
}
