namespace Parking.Domain.ValueObjects;

public class LicensePlate
{
    public string Value { get; }

    protected LicensePlate() { }

    public LicensePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("License plate is required");

        Value = value.ToUpper().Trim();
    }

    public override string ToString() => Value;
}
