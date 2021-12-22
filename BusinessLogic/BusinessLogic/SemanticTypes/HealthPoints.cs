namespace BusinessLogic.SemanticTypes;

public class HealthPoints
{
	readonly INotificationHandler<int>? healthPointsChanged;
	int current;

	public HealthPoints(int current, INotificationHandler<int>? healthPointsChanged)
	{
		this.healthPointsChanged = healthPointsChanged;
		Current = current;
		Max = current;
	}

	public int Current
	{
		get => current;
		private set
		{
			current = value;
			healthPointsChanged?.Notify(value);
		}
	}

	public int Max { get; }

	public bool ZeroOrBelow => Current <= 0;

	public int Damage(Damage damage)
	{
		var then = Current;
		Current -= damage.Value;
		return then - Current;
	}

	public int Heal(Heal heal)
	{
		var then = Current;
		Current = Math.Min(Max, Current + heal.Value);
		return Current - then;
	}
}