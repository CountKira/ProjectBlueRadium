using System;

namespace BusinessLogic.Effects
{
	public class HealEffect : IEffect
	{
		readonly int heal;

		public HealEffect(int heal)
		{
			this.heal = heal;
		}
		/// <inheritdoc />
		public string ActOn(Player subject)
		{
			var originalHp = subject.HealthPoints;
			var newHp = subject.HealthPoints + heal;
			subject.HealthPoints = Math.Min(newHp, subject.MaxHealthPoints);
			return $"Player was healed by {subject.HealthPoints - originalHp}";
		}
	}
}