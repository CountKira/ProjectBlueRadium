using BusinessLogic;

namespace SharedViewResources;

public class SystemRandom : IRandom
{
	readonly Random random = new();

	/// <inheritdoc />
	public int Next(int i) => random.Next(i);
}