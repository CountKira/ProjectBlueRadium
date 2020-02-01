namespace BusinessLogic.Effects {
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
}