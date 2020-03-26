using System.Collections.Generic;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Player : Creature
	{
		/// <inheritdoc />
		public Player(INotificationHandler<Creature, int>? healthPointsChanged = null) :
			base("The player", "The hero of our story", 10, 2, healthPointsChanged)
		{ }

		public ItemCollection Equipment { get; } = new ItemCollection();
		public ItemCollection Inventory { get; } = new ItemCollection();

		public bool IsDead() => HealthPoints <= 0;

	}
}