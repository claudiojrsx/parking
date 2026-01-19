namespace Parking.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    protected User() { } // EF

    public User(string name, string email, string passwordHash, Guid roleId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        RoleId = roleId;
    }
}
