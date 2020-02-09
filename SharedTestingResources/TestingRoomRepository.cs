using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedTestingResources
{
	public class TestingRoomRepository : IRoomRepository
	{
		readonly Room[] rooms =
		{
			new Room("You are in an empty room. The walls are smooth.",
				new[]
				{
					new Passage(1, "north"),
					new Passage(2, "west")
				}, new ItemCollection
				{
					new Item("poison", "This is a glass bottle, with a green substance inside it.",
						new[] { new ConsumableTag(new DamageEffect(50)), }),
					new Item("potion", "This is a glass bottle, with a red substance inside it.",
						new[] { new ConsumableTag(new HealEffect(10)),  }),
					new Item("book", "The book contains the story of boatmurdered."),
					new Item("fireball spell book",
						"The book contains the teachings to learn the spell fireball.")
				}),
			new Room("You are in a dark room.",
				new[]
				{
					new Passage(0, "south")
				},
				new ItemCollection(), new[]
				{
					new Creature("Evil guy", "The evil threat of the campaign.", 4, 2, new []{new MarkerTag(Tag.GameEnd), })
				}),
			new Room("You are in a bright room.",
				new[]
				{
					new Passage(0, "east")
				}, new ItemCollection
				{
					new Item("sword", "A sharp sword.", new[] {new MarkerTag(Tag.Weapon), }),
					new Item("shield", "A shield", new[] {new MarkerTag(Tag.Weapon),})
				})
		};

		/// <inheritdoc />
		public Room GetStartRoom() => rooms.FirstOrDefault();

		/// <inheritdoc />
		public Room GetRoomById(int id) => rooms[id];
	}
}