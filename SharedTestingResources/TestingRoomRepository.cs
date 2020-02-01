﻿using System.Linq;
using BusinessLogic;

namespace SharedTestingResources
{
	public class TestingRoomRepository : IRoomRepository
	{
		readonly Room[] rooms =
		{
			new Room("You are in an empty room. The walls are smooth.",
				new ItemCollection
				{
					new Item("bottle", "This is a glass bottle, with a green substance inside it.",
						new[] { Tag.Consumable }),
					new Item("book", "The book contains the story of boatmurdered."),
					new Item("fireball spell book",
						"The book contains the teachings to learn the spell fireball.")
				},
				new[]
				{
					new Passage(1, "north"),
					new Passage(2, "west")
				}),
			new Room("You are in a dark room.",
				new ItemCollection(),
				new[]
				{
					new Passage(0, "south")
				},
				new[]
				{
					new Creature("Evil guy", "The evil threat of the campaign.", 4, 2)
				}),
			new Room("You are in a bright room.",
				new ItemCollection
				{
					new Item("sword", "A sharp sword.", new[] {Tag.Weapon}),
					new Item("shield", "A shield", new[] {Tag.Weapon})
				},
				new[]
				{
					new Passage(0, "east")
				})
		};

		/// <inheritdoc />
		public Room GetStartRoom() => rooms.FirstOrDefault();

		/// <inheritdoc />
		public Room GetRoomById(int id) => rooms[id];
	}
}