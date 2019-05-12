using System;
using System.Linq;
using BusinessLogic;

namespace Views.ConsoleGui
{
	class ConsoleWriter : IWriter
	{
		/// <inheritdoc />
		public void WriteTextOutput(string text) => WriteLine(text);

		/// <inheritdoc />
		public void SetInvalidCommand(InvalidCommand invalidCommand)
		{
			switch (invalidCommand.CommandType)
			{
				case InvalidCommandType.EnemyNotFound:
				case InvalidCommandType.ItemNotFound:
				case InvalidCommandType.EntityNotFound:
					WriteLine($"There is no {invalidCommand.Specifier} in this room.");
					break;
				case InvalidCommandType.UnknownCommand:
					WriteLine("Unknown command.");
					break;
				case InvalidCommandType.PassageNotFound:
					WriteLine($"There is no {invalidCommand.Specifier} to go to.");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <inheritdoc />
		public void YouDiedByPoison()
		{
			WriteLine("This was not a good idea. The bottle contained poison. You're dead.");
		}

		/// <inheritdoc />
		public void DisplaySpells(bool spellKnown)
		{
			WriteLine(spellKnown ? "Fireball" : "You have no spells.");
		}

		/// <inheritdoc />
		public void LearnedFireball()
		{
			WriteLine("You learned the spell fireball.");
		}

		/// <inheritdoc />
		public void WriteAction(ActionDTO actionDto)
		{
			switch (actionDto.Verb)
			{
				case Verb.Drink:
					break;
				case Verb.Look:
					break;
				case Verb.Get:
					WriteLine($"You pick up the {actionDto.Specifier}.");
					break;
			}
		}

		/// <inheritdoc />
		public void WriteSeenObjects(SeenObjects seen)
		{
			var itemConcat = ItemCollectionToString.GetItemNameConcat(seen.Items);
			var items = seen.Items.Any() ? $" You see {itemConcat} on the floor." : "";
			var creature = seen.Creatures.FirstOrDefault()?.Name;
			var creatureS = creature == null ? "" : $" There is a {creature} standing in the room.";
			var passageMessage =
				seen.Passages.Length > 0
					? $" There is a passage {string.Join(" and ", seen.Passages.Select(p => p.DisplayName))}."
					: "";
			var result = $"{seen.EntityDescription}{passageMessage}{creatureS}{items}";
			WriteLine(result);
		}

		/// <inheritdoc />
		public void DescribeSelf(string description)
		{
			WriteLine(description);
		}

		/// <inheritdoc />
		public void ShowInventory(ItemCollection inventory)
		{
			var items = ItemCollectionToString.GetItemNameConcat(inventory);
			WriteLine($"You have {items}.");
		}

		/// <inheritdoc />
		public void ShowEquipment(ItemCollection equipment)
		{
			var items = ItemCollectionToString.GetItemNameConcat(equipment);
			WriteLine($"You have {items} equipped.");
		}

		private static void WriteLine(string text) =>
			Console.WriteLine(StringSplitter.BreakText(text, Console.WindowWidth));
	}
}