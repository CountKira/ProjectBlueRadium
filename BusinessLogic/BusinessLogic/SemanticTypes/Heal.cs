namespace BusinessLogic.SemanticTypes
{
	public readonly struct Heal
	{
		public Heal(int damage)
		{
			Value = damage;
		}
		public int Value { get; }

		public static implicit operator Heal(int value) => new Heal(value);
	}
}