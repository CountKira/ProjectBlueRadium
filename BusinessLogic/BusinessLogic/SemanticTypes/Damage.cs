using System;

namespace BusinessLogic.SemanticTypes
{
	public readonly struct Damage
	{
		public Damage(int damage)
		{
			if (damage < 0)
				throw new ArgumentOutOfRangeException(
					$"{nameof(damage)} can not be lower than 0 in {nameof(Damage)}. Value = {damage}.");
			Value = damage;
		}

		public int Value { get; }
	}
}