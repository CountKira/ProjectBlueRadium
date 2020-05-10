namespace BusinessLogic.Tags {
	public class WeaponTag : ITag
	{
		public WeaponTag(int damage)
		{
			Damage = damage;
		}

		public int Damage { get; }
	}
}