using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BusinessLogic;
using BusinessLogic.Effects;

namespace Views.ConsoleGui
{
	class ConsoleTestRoom : IRoomRepository
	{
		Dictionary<int, Room> rooms;

		public ConsoleTestRoom()
		{
			rooms = new Dictionary<int, Room>()
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
						CreatureFactory.Goblin(new []{Tag.GameEnd}),
					});
			}
		}

		static class CreatureFactory
		{
			public static Creature Goblin(IEnumerable<Tag>? tags = null) => new Creature("goblin", "a little green man", 4, 2, tags);
		}

		static class ItemFactory
		{
			public static Item HealingPotion() => new Item("healing potion", "A healing potion",
				new[] { Tag.Consumable },
				new Dictionary<Tag, IEffect>
				{
					{Tag.Consumable, new HealEffect(5)}
				});

			public static Item Dagger()
			{
				return new Item("dagger", "A Dagger", new[] { Tag.Weapon });
			}
		}
	}
}