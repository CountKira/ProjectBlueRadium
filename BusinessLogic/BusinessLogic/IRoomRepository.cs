namespace BusinessLogic;

public interface IRoomRepository
{
	Room GetStartRoom();
	Room GetRoomById(RoomId id);
}