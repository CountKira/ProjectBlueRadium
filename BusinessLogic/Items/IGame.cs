namespace BusinessLogic.Items
{
	public interface IGame
	{
		void WriteDescription(string description);
		void PickUpItem(Item item);
		void YouDiedByPoison();
		void Stop();
		void WriteAction(ActionDTO actionDTO);
		ItemCollection Inventory { get; }
	}
}