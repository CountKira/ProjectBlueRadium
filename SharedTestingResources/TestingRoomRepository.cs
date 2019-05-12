using System.Linq;
using BusinessLogic;
using SharedTestingResources.Items;

namespace SharedTestingResources
{
	public class TestingRoomRepository : IRoomRepository
	{
		private readonly Room[] rooms =
		{
			new Room("You are in an empty room. The walls are smooth.",
				new ItemCollection
				{
					new Bottle(),
					new SimpleItem("book", "The book contains the story of boatmurdered."),
					new SimpleItem("fireball spell book",
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
					new Creature("Evil guy", "The evil threat of the campaign.")
				}),
			new Room("You are in a bright room.",
				new ItemCollection
				{
					new SimpleItem("sword", "A sharp sword.", new[] {"weapon"}),
					new SimpleItem("shield", "A shield", new[] {"weapon"})
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