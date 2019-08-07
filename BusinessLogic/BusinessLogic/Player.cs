namespace BusinessLogic
{
	public class Player
	{
		public ItemCollection Equipment { get; } = new ItemCollection();
		public ItemCollection Inventory { get; } = new ItemCollection();

		public int HitPoints { get; set; } = 4;

		public bool IsDead() => HitPoints <= 0;
	}
}