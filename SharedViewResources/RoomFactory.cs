using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	static class RoomFactory
	{
		public static Room.Builder StartingRoom() =>
			new("Start room with basic equipment.",
				new()
				{
					ItemFactory.HealingPotion(),
					ItemFactory.HealingPotion(),
				});

		public static Room.Builder FirstChallengeRoom() =>
			new("First challenge.",
				creatures: new[]
				{
					CreatureFactory.Goblin(new[] {ItemFactory.Key(new(0)),}),
				});

		public static Room.Builder SecondChallengeRoom() =>
			new("Second challenge.",
				creatures: new[]
				{
					CreatureFactory.Goblin(tags: new[] {new MarkerTag(Tag.GameEnd),}),
				});
	}
}