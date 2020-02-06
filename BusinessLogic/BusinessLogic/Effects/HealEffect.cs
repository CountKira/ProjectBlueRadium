using System;

namespace BusinessLogic.Effects {
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
			var originalHp = subject.HitPoints;
			var newHp = subject.HitPoints + heal;
			subject.HitPoints = Math.Min(newHp, subject.MaxHitPoints);
			return $"Player was healed by {subject.HitPoints - originalHp}";
		}
	}
}