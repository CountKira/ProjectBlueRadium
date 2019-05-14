namespace BusinessLogic
{
	public interface IGame
	{
		void AddToPlayerInventory(Item item);
		void YouDiedByPoison();
		void Stop();
		void WriteAction(ActionDTO actionDTO);
	}
}