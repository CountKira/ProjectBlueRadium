using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace Views.ConsoleGui
{
	class ConsoleTestRoom : IRoomRepository
	{
		readonly Dictionary<int, Room> rooms;

		public ConsoleTestRoom()
		{
			rooms = new Dictionary<int, Room>
			{
				{0, RoomFactory.StartingRoom()},
				{1, RoomFactory.FirstChallengeRoom()},
				{2, RoomFactory.SecondChallengeRoom()},
				//{3, new Room("Better gear")},
				//{4, new Room("Third challenge")},
				//{5, new Room("Healing potions")},
				//{6, new Room("Stage end boss")},
			};
		}

		/// <inheritdoc />
		public Room GetStartRoom() => rooms[0];

		/// <inheritdoc />
		public Room GetRoomById(int id) => rooms[id];

		static class RoomFactory
		{
			public static Room StartingRoom() =>
				new Room("Start room with basic equipment.",
					new[] { new Passage(1, "north"), },
					new ItemCollection
					{
						ItemFactory.Dagger(),
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

		static class CreatureFactory
		{
			public static Creature Goblin(IEnumerable<ITag>? tags = null) => new Creature("goblin", "a little green man", 4, 2, tags);
		}

		static class ItemFactory
		{
			public static Item HealingPotion() => new Item("healing potion", "A healing potion",
				new[] { new ConsumableTag(new HealEffect(5)), });

			public static Item Dagger()
			{
				return new Item("dagger", "A Dagger", new[] { new MarkerTag(Tag.Weapon), });
			}
		}
	}
}