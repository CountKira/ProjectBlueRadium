using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	static class RoomFactory
	{
		public static Room StartingRoom() =>
			new Room("Start room with basic equipment.",
				new[] { new Passage(1, "north"), },
				new ItemCollection
				{
					ItemFactory.Dagger(),
					ItemFactory.Sword(),
					ItemFactory.HealingPotion(),
					ItemFactory.HealingPotion(),
				});

		public static Room FirstChallengeRoom()
		{
			return new Room(
				"First challenge.",
				new[] { new Passage(0, "south"), new Passage(2, "north") },
				creatures: new[]
				{
					CreatureFactory.Goblin(),
				});
		}

		public static Room SecondChallengeRoom()
		{
			return new Room(
				"Second challenge.",
				new[] { new Passage(1, "south") },
				creatures: new[]
				{
					CreatureFactory.Goblin(new []{new MarkerTag(Tag.GameEnd), }),
				});
		}
	}
}