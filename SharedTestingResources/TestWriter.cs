using System.Collections.Generic;
using BusinessLogic;

namespace SharedTestingResources
{
	public class TestWriter : IWriter
	{
		public Queue<string> TextOutput { get; } = new Queue<string>();
		public bool SpellKnown { get; private set; }
		public bool FireballLearned { get; private set; }
		public InvalidCommand InvalidCommand { get; private set; }
		public OutputData Action { get; private set; }
		public SeenObjects SeenThings { get; private set; }
		public string Me { get; private set; }
		public ItemCollection Inventory { get; private set; }
		public ItemCollection Equipment { get; private set; }

		/// <inheritdoc />
		public void WriteTextOutput(string text)
		{
			TextOutput.Enqueue(text);
		}

		/// <inheritdoc />
		public void SetInvalidCommand(InvalidCommand invalidCommand)
		{
			InvalidCommand = invalidCommand;
		}

		/// <inheritdoc />
		public void DisplaySpells(bool spellKnown)
		{
			SpellKnown = spellKnown;
		}

		/// <inheritdoc />
		public void Write(OutputData outputData)
		{
			if (outputData.Type == OutputDataType.LearnedSpell)
				FireballLearned = true;
			else
				Action = outputData;
		}

		/// <inheritdoc />
		public void WriteSeenObjects(SeenObjects seen)
		{
			SeenThings = seen;
		}

		/// <inheritdoc />
		public void DescribeSelf(string description)
		{
			Me = description;
		}

		/// <inheritdoc />
		public void ShowInventory(ItemCollection inventory)
		{
			Inventory = inventory;
		}

		/// <inheritdoc />
		public void ShowEquipment(ItemCollection equipment)
		{
			Equipment = equipment;
		}
	}
}