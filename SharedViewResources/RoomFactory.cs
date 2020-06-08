using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	static class RoomFactory
	{
		public static Room.Builder StartingRoom() =>
			new Room.Builder("Start room with basic equipment.",
				new ItemCollection
				{
					ItemFactory.Dagger(),
					ItemFactory.Sword(),
					ItemFactory.HealingPotion(),
					ItemFactory.HealingPotion(),
				});

		public static Room.Builder FirstChallengeRoom() =>
			new Room.Builder("First challenge.",
				creatures: new[]
				{
					CreatureFactory.Goblin(new []{ItemFactory.Key(0)}),
				});

		public static Room.Builder SecondChallengeRoom() =>
			new Room.Builder("Second challenge.",
				creatures: new[]
				{
					CreatureFactory.Goblin(tags:new []{new MarkerTag(Tag.GameEnd)}),
				});
	}
}