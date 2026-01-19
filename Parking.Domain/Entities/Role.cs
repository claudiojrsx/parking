namespace Parking.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    protected Role() { } // EF

    public Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
