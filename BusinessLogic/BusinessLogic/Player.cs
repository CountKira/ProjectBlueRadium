namespace BusinessLogic
{
	public class Player
	{
		public ItemCollection Equipment { get; } = new ItemCollection();
		public ItemCollection Inventory { get; } = new ItemCollection();

		public int HitPoints { get; set; } = 10;
		public int MaxHitPoints { get; } = 10;

		public bool IsDead() => HitPoints <= 0;
	}
}