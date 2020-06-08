using System;

namespace BusinessLogic.SemanticTypes
{
	public readonly struct Heal
	{
		public Heal(int heal)
		{
			if (heal < 0)
			{
				throw new ArgumentOutOfRangeException($"{nameof(heal)} can not be lower than 0 in {nameof(Heal)}. Value = {heal}.");
			}
			Value = heal;
		}
		public int Value { get; }

		public static implicit operator Heal(int value) => new Heal(value);
	}
}