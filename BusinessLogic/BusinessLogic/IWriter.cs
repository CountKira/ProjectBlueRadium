namespace BusinessLogic
{
	public interface IWriter
	{
		void WriteTextOutput(string text);
		void SetInvalidCommand(InvalidCommand invalidCommand);
		void YouDiedByPoison();
		void DisplaySpells(bool spellKnown);
		void LearnedFireball();
		void WriteAction(ActionDTO actionDto);
		void WriteSeenObjects(SeenObjects seen);
		void DescribeSelf(string description);
		void ShowInventory(ItemCollection inventory);
		void ShowEquipment(ItemCollection equipment);
	}
}