using System.Collections.Generic;
using BusinessLogic;

namespace SharedTestingResources
{
	public class TestWriter : IWriter
	{
		public Queue<string> TextOutput { get; } = new Queue<string>();
		public bool DiedPyPoison { get; private set; }
		public bool SpellKnown { get; private set; }
		public bool FireballLearned { get; private set; }
		public InvalidCommand InvalidCommand { get; private set; }
		public ActionDTO Action { get; private set; }
		public SeenObjects SeenThings { get; private set; }
		public string Me { get; private set; }
		public ItemCollection Inventory { get; private set; }
		public ItemCollection Equipment { get; private set; }

		public int HealthPoints { get; private set; }

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
		public void YouDiedByPoison()
		{
			DiedPyPoison = true;
		}

		/// <inheritdoc />
		public void DisplaySpells(bool spellKnown)
		{
			SpellKnown = spellKnown;
		}

		/// <inheritdoc />
		public void LearnedFireball()
		{
			FireballLearned = true;
		}

		/// <inheritdoc />
		public void WriteAction(ActionDTO actionDto)
		{
			Action = actionDto;
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

		/// <inheritdoc />
		public void ShowHealthPoints(int healthPoints)
		{
			HealthPoints = healthPoints;
		}
	}
}