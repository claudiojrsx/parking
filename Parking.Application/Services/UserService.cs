using Parking.Application.Interfaces.Repositories;
using Parking.Domain.Entities;

namespace Parking.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<Guid> CreateUserAsync(string name, string email, string password, Guid roleId)
        {
            var exists = await _userRepo.GetByEmailAsync(email);
            if (exists != null)
                throw new InvalidOperationException("User with this email already exists.");

            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(name, email, hash, roleId);
            await _userRepo.AddAsync(user);

            return user.Id;
        }
    }
}
