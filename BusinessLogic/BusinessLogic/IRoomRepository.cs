namespace BusinessLogic
{
	public interface IRoomRepository
	{
		Room GetStartRoom();
		Room GetRoomById(int id);
	}
}