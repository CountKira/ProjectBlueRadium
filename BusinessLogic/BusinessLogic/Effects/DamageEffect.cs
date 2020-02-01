using System;
using System.Text.RegularExpressions;

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
		public void ActOn(Player subject)
		{
			subject.HitPoints -= damage;
		}
	}
	public class HealEffect : IEffect
	{
		readonly int heal;

		public HealEffect(int heal)
		{
			this.heal = heal;
		}
		/// <inheritdoc />
		public void ActOn(Player subject)
		{
			var newHp = subject.HitPoints + heal;
			subject.HitPoints = Math.Min(newHp, subject.MaxHitpoints);
		}
	}
}