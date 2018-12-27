using System;
using System.Linq;
using BusinessLogic;

namespace Views.ConsoleGui
{
	class ConsoleWriter : IWriter
	{
		/// <inheritdoc />
		public void WriteDescription(string text)
		{
			Console.WriteLine(StringSplitter.BreakText(text, Console.WindowWidth));
		}

		/// <inheritdoc />
		public void SetInvalidCommand(InvalidCommand invalidCommand)
		{
			switch (invalidCommand.CommandType)
			{
				case InvalidCommandType.ItemNotFound:
					WriteDescription($"There is no {invalidCommand.Specifier} in this room.");
					break;
				case InvalidCommandType.UnknownCommand:
					WriteDescription("Unknown command.");
					break;
				case InvalidCommandType.PassageNotFound:
					WriteDescription($"There is no {invalidCommand.Specifier} to go to.");
					break;
				default:
					throw new ArgumentOutOfRangeException();

			}
		}

		/// <inheritdoc />
		public void YouDiedByPoison()
		{
			WriteDescription("This was not a good idea. The bottle contained poison. You're dead.");
		}

		/// <inheritdoc />
		public void DisplaySpells(bool spellKnown)
		{
			WriteDescription(spellKnown ? "Fireball" : "You have no spells.");
		}

		/// <inheritdoc />
		public void LearnedFireball()
		{
			WriteDescription("You learned the spell fireball.");
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
					WriteDescription($"You pick up the {actionDto.Specifier}.");
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
				seen.Passages.Length > 0 ? $" There is a passage {string.Join(" and ", seen.Passages.Select(p => p.DisplayName))}." : "";
			var result = $"{seen.EntityDescription}{passageMessage}{creatureS}{items}";
			WriteDescription(result);
		}

		/// <inheritdoc />
		public void DescribeSelf(string description)
		{
			WriteDescription(description);
		}

		/// <inheritdoc />
		public void ShowInventory(ItemCollection inventory)
		{
			var items = ItemCollectionToString.GetItemNameConcat(inventory);
			WriteDescription($"You have {items}.");
		}
	}
}
