namespace Parking.Domain.ValueObjects;

public sealed class LicensePlate : IEquatable<LicensePlate>
{
    public string Value { get; }

    protected LicensePlate() { }

    public LicensePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("License plate is required");

        Value = value.Trim().ToUpper();
    }

    public bool Equals(LicensePlate? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as LicensePlate);

    public override int GetHashCode()
        => Value.GetHashCode();
}
