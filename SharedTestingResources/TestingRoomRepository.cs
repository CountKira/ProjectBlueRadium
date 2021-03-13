using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedTestingResources
{
	public class TestingRoomRepository : IRoomRepository
	{
		readonly Dictionary<RoomId, Room> rooms;

		public TestingRoomRepository()
		{
			var mapBuilder = new MapBuilder();

			var builders = new[]
			{
				mapBuilder.AddRoomBuilder(RoomFactory.StartRoom),
				mapBuilder.AddRoomBuilder(RoomFactory.DarkRoom),
				mapBuilder.AddRoomBuilder(RoomFactory.BrightRoom),
			};

			mapBuilder.ConnectRooms(builders[0],"north",builders[1],"south");
			mapBuilder.ConnectRooms(builders[0],"west",builders[2],"east");

			rooms = mapBuilder.Build();
		}
		static class ItemFactory
		{
			internal static Item Poison => new Item("poison", "This is a glass bottle, with a green substance inside it.",
				new[] { new ConsumableTag(new DamageEffect(50)) });
			internal static Item Potion => new Item("potion", "This is a glass bottle, with a red substance inside it.",
				new[] { new ConsumableTag(new HealEffect(10))});
			internal static Item Book => new Item("book", "The book contains the story of boatmurdered.");
			internal static Item FireballSpellBook => new Item("fireball spell book", "The book contains the teachings to learn the spell fireball.");
			internal static Item Sword => new Item("sword", "A sharp sword.", new[] { new WeaponTag(2)});
			internal static Item Shield => new Item("shield", "A shield", new[] { new WeaponTag(0)});
		}

		static class RoomFactory
		{
			internal static Room.Builder StartRoom => new Room.Builder("You are in an empty room. The walls are smooth.", new ItemCollection
			{
				ItemFactory.Poison,
				ItemFactory.Potion,
				ItemFactory.Book,
				ItemFactory.FireballSpellBook,
			});
			internal static Room.Builder DarkRoom => new Room.Builder("You are in a dark room.", creatures: new[]
			{
				new Creature("Evil guy", "The evil threat of the campaign.", 4, 2, tags: new []{new MarkerTag(Tag.GameEnd)}),
			});
			internal static Room.Builder BrightRoom => new Room.Builder("You are in a bright room.", new ItemCollection
			{
				ItemFactory.Sword,
				ItemFactory.Shield,
			});
		}

		readonly RoomId startRoomId = new(0);

		/// <inheritdoc />
		public Room GetStartRoom() => rooms[startRoomId];

		/// <inheritdoc />
		public Room GetRoomById(RoomId id) => rooms[id];
	}
}