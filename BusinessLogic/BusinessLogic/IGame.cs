namespace BusinessLogic
{
	public interface IGame
	{
		ItemCollection Inventory { get; }
		void WriteDescription(string description);
		void PickUpItem(Item item);
		void YouDiedByPoison();
		void Stop();
		void WriteAction(ActionDTO actionDTO);
	}
}