using System;
using BusinessLogic.SemanticTypes;

namespace BusinessLogic.Effects
{
	public class HealEffect : IEffect
	{
		readonly Heal heal;

		public HealEffect(Heal heal)
		{
			this.heal = heal;
		}
		/// <inheritdoc />
		public string ActOn(Creature subject)
		{
			return $"Player was healed by {subject.HealthPoints.Heal(heal)}";
		}
	}
}