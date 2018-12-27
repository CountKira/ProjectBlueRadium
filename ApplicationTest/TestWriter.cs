using System.Collections.Generic;
using BusinessLogic;

namespace ApplicationTest
{
	class TestWriter : IWriter
	{
		public string Description { get; private set; }
		public bool DiedPyPoison { get; private set; }
		public bool SpellKnown { get; private set; }
		public bool FireballLearned { get; private set; }
		public InvalidCommand InvalidCommand { get; private set; }
		public ActionDTO Action { get; private set; }
		public SeenObjects SeenThings { get; private set; }
		public string Me { get; private set; }
		public ItemCollection Inventory { get; private set; }

		/// <inheritdoc />
		public void WriteDescription(string text)
		{
			Description = text;
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
	}
}
