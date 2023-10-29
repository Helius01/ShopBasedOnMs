namespace ShopBasedOnMs.BuildingBlocks.TypeProvider;

public readonly record struct ConnectionString
{
    private readonly string Value;

    public ConnectionString(string value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ConnectionString conn) => conn.Value;
    public static explicit operator ConnectionString(string value) => new(value);
}