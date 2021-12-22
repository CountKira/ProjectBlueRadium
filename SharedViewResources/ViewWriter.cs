using BusinessLogic;

namespace SharedViewResources;

public class ViewWriter : IWriter
{
	readonly IViewWriter writer;

	public ViewWriter(IViewWriter writer) => this.writer = writer;

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
			case InvalidCommandType.PortalNotFound:
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
			case InvalidCommandType.MissingKey:
				WriteLine("Matching key is missing.");
				break;
			case InvalidCommandType.NotLocked:
				WriteLine($"The way {invalidCommand.Specifier} is not locked.");
				break;
			case InvalidCommandType.Locked:
				WriteLine("Locked.");
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
	public void Write(OutputData outputData)
	{
		switch (outputData.Type)
		{
			case OutputDataType.LearnedSpell:
				WriteLine($"You learned the spell {outputData.Specifier}.");
				break;
			case OutputDataType.Drink:
			case OutputDataType.Look:
				break;
			case OutputDataType.Get:
				WriteLine($"You pick up the {outputData.Specifier}.");
				break;
			case OutputDataType.Wield:
				WriteLine($"You wield the {outputData.Specifier}.");
				break;
			case OutputDataType.Unwield:
				WriteLine($"You removed the {outputData.Specifier}.");
				break;
			case OutputDataType.Unlocked:
				WriteLine($"Unlocked the way {outputData.Specifier}.");
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
		var creatureDescription = GetCreatureDescription(creature);

		var portal =
			seen.Portals.Length > 0
				? $" There is a door {string.Join(" and ", seen.Portals.Select(p => p.DisplayName))}."
				: "";

		WriteLine($"{seen.EntityDescription}{portal}{creatureDescription}{items}");
	}

	/// <inheritdoc />
	public void DescribeSelf(string description)
	{
		WriteLine(description);
	}

	/// <inheritdoc />
	public void ShowInventory(IEnumerable<Item> inventory)
	{
		var items = ItemCollectionToString.GetItemNameConcat(inventory);
		WriteLine(
			string.IsNullOrEmpty(items)
				? "Your inventory is empty."
				: $"You have {items}.");
	}

	/// <inheritdoc />
	public void ShowEquipment(IEnumerable<Item> equipment)
	{
		var items = ItemCollectionToString.GetItemNameConcat(equipment);
		WriteLine(
			string.IsNullOrEmpty(items)
				? "You have nothing equipped"
				: $"You have {items} equipped.");
	}

	static string GetCreatureDescription(Creature? creature)
	{
		if (creature is null)
			return "";
		var creatureStatus = creature.IsDead ? "dead " : "";
		var creatureInventory = GetCreatureInventoryText(creature);
		return $" There is a {creatureStatus}{creature.Name} in the room{creatureInventory}.";
	}

	static string GetCreatureInventoryText(Creature creature)
	{
		if (!creature.IsDead)
			return "";
		var inventoryText = ItemCollectionToString.GetItemNameConcat(creature.GetInventoryItems());
		return string.IsNullOrEmpty(inventoryText) ? "" : $" with {inventoryText}";
	}

	void WriteLine(string text) => writer.WriteLine(text);
}