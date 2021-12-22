using BusinessLogic.Tags;

namespace BusinessLogic;

public class MapBuilder
{
	readonly List<Room.Builder> roomBuilders = new();
	int roomCounter;

	public Room.Builder AddRoomBuilder(Room.Builder builder)
	{
		builder.RoomId = new(roomCounter++);
		roomBuilders.Add(builder);
		return builder;
	}

	public static void ConnectRooms(Room.Builder room1, PortalName portalName1, Room.Builder room2,
		PortalName portalName2,
		IEnumerable<ITag>? tags = null)
	{
		var passage = new Passage.Builder(tags);

		var portal1 = new Portal.Builder(passage, portalName1);
		room1.AddPortal(portal1);

		var portal2 = new Portal.Builder(passage, portalName2);
		room2.AddPortal(portal2);
	}

	public Dictionary<RoomId, Room> Build()
	{
		var dictionary = new Dictionary<RoomId, Room>();
		foreach (var roomBuilder in roomBuilders)
		{
			var room = roomBuilder.Build();
			dictionary.Add(
				roomBuilder.RoomId ??
				throw new InvalidOperationException($"{nameof(roomBuilder.RoomId)} is not set"), room);
		}

		return dictionary;
	}
}