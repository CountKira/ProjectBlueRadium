﻿namespace BusinessLogic
{
	public interface IWriter
	{
		void WriteTextOutput(string text);
		void SetInvalidCommand(InvalidCommand invalidCommand);
		void DisplaySpells(bool spellKnown);
		void Write(OutputData outputData);
		void WriteSeenObjects(SeenObjects seen);
		void DescribeSelf(string description);
		void ShowInventory(ItemCollection inventory);
		void ShowEquipment(ItemCollection equipment);
	}
}