using Microsoft.EntityFrameworkCore;
using Parking.Infrastructure.Persistence;

namespace Parking.Infrastructure.Repositories;

public class Repository<T>(ParkingDbContext context) : IRepository<T> where T : class
{
    protected readonly ParkingDbContext _context = context;

    public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    public async Task<T?> GetByIdAsync(Guid id)
        => await _context.Set<T>().FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().ToListAsync();

    public void Update(T entity)
        => _context.Set<T>().Update(entity);

    public void Remove(T entity)
        => _context.Set<T>().Remove(entity);
}
