using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;

namespace Parking.Application.Services
{
    public class UserService(IUserRepository userRepo)
    {
        private readonly IUserRepository _userRepo = userRepo;

        public async Task<Guid> CreateUserAsync(string name, string email, string password, Guid roleId, bool isActive)
        {
            var exists = await _userRepo.GetByEmailAsync(email);
            if (exists != null)
                throw new InvalidOperationException("User with this email already exists.");

            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(name, email, hash, roleId, isActive);
            await _userRepo.AddAsync(user);

            return user.Id;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }
    }
}
