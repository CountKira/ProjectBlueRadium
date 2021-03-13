﻿using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	public class ConsoleTestRoom : IRoomRepository
	{
		readonly Dictionary<RoomId, Room> rooms;

		readonly RoomId startRoomId = new(0);

		public ConsoleTestRoom()
		{
			var mapBuilder = new MapBuilder();
			var startRoom = mapBuilder.AddRoomBuilder(RoomFactory.StartingRoom());
			var firstChallenge = mapBuilder.AddRoomBuilder(RoomFactory.FirstChallengeRoom());
			var secondChallenge = mapBuilder.AddRoomBuilder(RoomFactory.SecondChallengeRoom());
			//{3, new Room("Better gear")},
			//{4, new Room("Third challenge")},
			//{5, new Room("Healing potions")},
			//{6, new Room("Stage end boss")},

			mapBuilder.ConnectRooms(startRoom, "north", firstChallenge, "south");
			mapBuilder.ConnectRooms(firstChallenge, "north", secondChallenge, "south", new[] {new LockTag(new(0)),});

			rooms = mapBuilder.Build();
		}

		/// <inheritdoc />
		public Room GetStartRoom() => rooms[startRoomId];

		/// <inheritdoc />
		public Room GetRoomById(RoomId id) => rooms[id];
	}
}