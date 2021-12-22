namespace BusinessLogic;

public abstract class OrdinalIgnoreCaseName
{
	protected OrdinalIgnoreCaseName(string value) => Value = value;

	public string Value { get; }

	protected bool Equals(OrdinalIgnoreCaseName other) => Value == other.Value;

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((OrdinalIgnoreCaseName) obj);
	}

	public override string ToString() => Value;

	public static bool operator ==(OrdinalIgnoreCaseName a, OrdinalIgnoreCaseName b) =>
		a.Value.Equals(b.Value, StringComparison.OrdinalIgnoreCase);

	public static bool operator !=(OrdinalIgnoreCaseName a, OrdinalIgnoreCaseName b) =>
		!a.Equals(b);

	public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
}