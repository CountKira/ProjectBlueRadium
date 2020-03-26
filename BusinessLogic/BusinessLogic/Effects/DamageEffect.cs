using System;

namespace BusinessLogic.Effects
{
	public class DamageEffect : IEffect
	{
		readonly int damage;

		public DamageEffect(int damage)
		{
			this.damage = damage;
		}
		/// <inheritdoc />
		public string ActOn(Player subject)
		{
			subject.HealthPoints -= damage;
			return $"Player was dealt {damage} damage";
		}
	}
}