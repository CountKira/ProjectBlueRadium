namespace BusinessLogic
{
	public interface IGame
	{
		ItemCollection Inventory { get; }
		void YouDiedByPoison();
		void Stop();
		void WriteAction(ActionDTO actionDTO);
	}
}