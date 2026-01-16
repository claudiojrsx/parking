namespace Parking.Domain.ValueObjects;

public sealed class LicensePlate : IEquatable<LicensePlate>
{
    public string Value { get; }

    public LicensePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("License plate is required");

        Value = value.ToUpperInvariant();
    }

    public bool Equals(LicensePlate? other)
        => other != null && Value == other.Value;

    public override bool Equals(object? obj)
        => Equals(obj as LicensePlate);

    public override int GetHashCode()
        => Value.GetHashCode();

    public static bool operator ==(LicensePlate left, LicensePlate right)
        => Equals(left, right);

    public static bool operator !=(LicensePlate left, LicensePlate right)
        => !Equals(left, right);

    public override string ToString() => Value;
}
