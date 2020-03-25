using System.Collections.Generic;
using BusinessLogic;

namespace SharedViewResources {
	public class ConsoleTestRoom : IRoomRepository
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

	}
}