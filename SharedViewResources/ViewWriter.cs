﻿using System;
using System.Linq;
using BusinessLogic;

namespace SharedViewResources {
	public class ViewWriter : IWriter
	{
		readonly IViewWriter writer;

		public ViewWriter(IViewWriter writer)
		{
			this.writer = writer;
		}
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
				case InvalidCommandType.CanNotWield:
					WriteLine($"Can not wield {invalidCommand.Specifier}, because it is not a weapon.");
					break;
				case InvalidCommandType.AlreadyWielding:
					WriteLine("Already wielding an weapon.");
					break;
				case InvalidCommandType.NotExecutable:
					WriteLine($"This action can not be performed with \"{invalidCommand.Specifier}\".");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
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
				case VerbEnum.Drink:
				case VerbEnum.Look:
					break;
				case VerbEnum.Get:
					WriteLine($"You pick up the {actionDto.Specifier}.");
					break;
				case VerbEnum.Wield:
					WriteLine($"You wield the {actionDto.Specifier}.");
					break;
				case VerbEnum.Unwield:
					WriteLine($"You removed the {actionDto.Specifier}.");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <inheritdoc />
		public void WriteSeenObjects(SeenObjects seen)
		{
			var itemConcat = ItemCollectionToString.GetItemNameConcat(seen.Items);
			var items = seen.Items.Any() ? $" You see {itemConcat} on the floor." : "";
			var creature = seen.Creatures.FirstOrDefault();
			var creatureStatus = creature?.HealthPoints > 0 ? "" : "dead ";
			var creatureS = creature == null ? "" : $" There is a {creatureStatus}{creature.Name} in the room.";
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
			if (inventory.Any())
			{
				var items = ItemCollectionToString.GetItemNameConcat(inventory);
				WriteLine($"You have {items}.");
			}
			else
			{
				WriteLine("Your inventory is empty.");
			}
		}

		/// <inheritdoc />
		public void ShowEquipment(ItemCollection equipment)
		{
			if (equipment.Any())
			{
				var items = ItemCollectionToString.GetItemNameConcat(equipment);
				WriteLine($"You have {items} equipped.");
			}
			else
			{
				WriteLine("You have nothing equipped");
			}
		}

		void WriteLine(string text) => writer.WriteLine(text);
	}
}