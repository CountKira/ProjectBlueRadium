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
				{0, StartingRoom()},
				{1, new Room("First challenge")},
				//{2, new Room("Second challenge")},
				//{3, new Room("Better gear")},
				//{4, new Room("Third challenge")},
				//{5, new Room("Healing potions")},
				//{6, new Room("Stage end boss")},
			};
		}

		static Item HealingPotion() => new Item("healing potion", "A healing potion",
			new[] {Tag.Consumable},
			new Dictionary<Tag, IEffect>
			{
				{Tag.Consumable, new HealEffect(5)}
			});

		static Room StartingRoom() =>
			new Room("Start room with basic equipment",
				itemsOnFloor: new ItemCollection
				{
					new Item("dagger", "A Dagger", new []{Tag.Weapon}),
					HealingPotion(),
					HealingPotion(),
				});

		/// <inheritdoc />
		public Room GetStartRoom() => rooms[0];

		/// <inheritdoc />
		public Room GetRoomById(int id) => rooms[id];
	}
}