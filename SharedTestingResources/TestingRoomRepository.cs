﻿using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedTestingResources;

public class TestingRoomRepository : IRoomRepository
{
	readonly Dictionary<RoomId, Room> rooms;

	readonly RoomId startRoomId = new(0);

	public TestingRoomRepository()
	{
		var mapBuilder = new MapBuilder();

		var builders = new[]
		{
			mapBuilder.AddRoomBuilder(RoomFactory.StartRoom),
			mapBuilder.AddRoomBuilder(RoomFactory.DarkRoom),
			mapBuilder.AddRoomBuilder(RoomFactory.BrightRoom),
		};

		MapBuilder.ConnectRooms(builders[0], BasicPortalName.North, builders[1], BasicPortalName.South);
		MapBuilder.ConnectRooms(builders[0], BasicPortalName.West, builders[2], BasicPortalName.East);

		rooms = mapBuilder.Build();
	}

	/// <inheritdoc />
	public Room GetStartRoom() => rooms[startRoomId];

	/// <inheritdoc />
	public Room GetRoomById(RoomId id) => rooms[id];

	static class ItemFactory
	{
		internal static Item Poison => new(new("poison"), "This is a glass bottle, with a green substance inside it.",
			new[] {new ConsumableTag(new DamageEffect(new(50))),});

		internal static Item Potion => new(new("potion"), "This is a glass bottle, with a red substance inside it.",
			new[] {new ConsumableTag(new HealEffect(new(10))),});

		internal static Item Book => new(new("book"), "The book contains the story of boatmurdered.");

		internal static Item FireballSpellBook => new(new("fireball spell book"),
			"The book contains the teachings to learn the spell fireball.");

		internal static Item Sword => new(new("sword"), "A sharp sword.", new[] {new WeaponTag(new(2)),});
		internal static Item Shield => new(new("shield"), "A shield", new[] {new WeaponTag(new(0)),});
	}

	static class RoomFactory
	{
		internal static Room.Builder StartRoom => new("You are in an empty room. The walls are smooth.", new()
		{
			ItemFactory.Poison,
			ItemFactory.Potion,
			ItemFactory.Book,
			ItemFactory.FireballSpellBook,
		});

		internal static Room.Builder DarkRoom => new("You are in a dark room.", creatures: new[]
		{
			new Creature(new("Evil guy"), "The evil threat of the campaign.", new(4, null), new(2),
				tags: new[] {new MarkerTag(Tag.GameEnd),}),
		});

		internal static Room.Builder BrightRoom => new("You are in a bright room.", new()
		{
			ItemFactory.Sword,
			ItemFactory.Shield,
		});
	}
}