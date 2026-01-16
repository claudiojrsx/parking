namespace Parking.Application.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
}
