using System;
using BusinessLogic.SemanticTypes;

namespace BusinessLogic.Effects
{
	public class DamageEffect : IEffect
	{
		readonly Damage damage;

		public DamageEffect(Damage damage)
		{
			this.damage = damage;
		}
		/// <inheritdoc />
		public string ActOn(Creature subject)
		{
			return $"Player was dealt {subject.HealthPoints.Damage(damage)} damage";
		}
	}
}