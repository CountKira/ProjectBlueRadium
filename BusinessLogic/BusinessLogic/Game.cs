namespace BusinessLogic;

public class Game : IGame
{
	readonly AttackHandler attackHandler;
	readonly CommandInterpreter commandInterpreter;
	readonly IRoomRepository roomRepository;
	readonly IWriter writer;
	bool hasActed;
	Room room;

	public Game(IWriter writer, IRoomRepository roomRepository, IRandom random, Creature player)
	{
		this.writer = writer;
		this.roomRepository = roomRepository;
		attackHandler = new(writer, random, this);
		commandInterpreter = new(writer, this);
		room = roomRepository.GetStartRoom();

		Player = player;
	}

	public bool IsRunning { get; private set; } = true;

	public Creature Player { get; }

	/// <inheritdoc />
	public void Stop() => IsRunning = false;

	public void PickUpItem(Item item)
	{
		writer.Write(new(OutputDataType.Get) {Specifier = item.Name.Value,});
		room.RemoveItem(item);
		AddToPlayerInventory(item);
		HasActed();
	}

	public bool TryGetPortal(PortalName portalName, out Portal? portal)
		=> room.TryGetPortal(portalName, out portal);

	public void GoToRoomById(RoomId roomId)
	{
		room = roomRepository.GetRoomById(roomId);
		writer.WriteSeenObjects(room.GetDescription());
	}

	public void HandleAttacking(CreatureName enemy)
	{
		var creature = room.GetCreature(enemy);
		if (creature is null || creature.IsDead)
		{
			writer.SetInvalidCommand(new(InvalidCommandType.EntityNotFound)
				{Specifier = enemy.Value,});
			return;
		}

		attackHandler.AttackCreature(Player, creature);
		HasActed();
	}

	/// <inheritdoc />
	public void HasActed() => hasActed = true;

	public string? GetLocalAvailableEntityDescription(string entityName)
	{
		var asItem = new ItemName(entityName);
		if (GetItemObjectInRoom(asItem) is { } floorItem)
			return floorItem.Description;

		if (Player.HasItem(asItem, out var item))
			return item!.Description;

		var creature = room.GetCreature(new(entityName));
		return creature?.Description;
	}

	/// <inheritdoc />
	public Room GetCurrentRoom() => room;

	/// <inheritdoc />
	public Item? GetItemFromPlayerInventory(ItemName itemName) => Player.GetItem(itemName);

	/// <inheritdoc />
	public void WieldWeapon(Item item)
	{
		if (Player.CanWieldWeapon)
		{
			writer.SetInvalidCommand(new(InvalidCommandType.AlreadyWielding));
		}
		else
		{
			writer.Write(new(OutputDataType.Wield) {Specifier = item.Name.Value,});
			Player.Equip(item);
		}
	}

	/// <inheritdoc />
	public bool UnwieldWeapon(ItemName itemName)
	{
		var item = Player.WieldsItem(itemName);
		var didUnwield = item != null && Player.Unwield(item);
		if (didUnwield)
			writer.Write(new(OutputDataType.Unwield) {Specifier = item!.Name.Value,});
		else
			writer.SetInvalidCommand(new(InvalidCommandType.EntityNotFound) {Specifier = itemName.Value,});

		return didUnwield;
	}

	/// <inheritdoc />
	public void LearnSpell()
	{
		Player.HasSpell = true;
		HasActed();
	}

	public Item? GetItemObjectInRoom(ItemName itemName) => room.GetItem(itemName);

	void AddToPlayerInventory(Item item) => Player.PutIntoInventory(item);

	void ExecuteOtherActors()
	{
		var creatures = room.GetCreatures();
		foreach (var creature in creatures.Where(c => !c.IsDead))
			attackHandler.GetAttackedByCreature(Player, creature);
	}

	public void EnterCommand(string text)
	{
		commandInterpreter.Interpret(text);

		if (hasActed)
		{
			hasActed = false;
			ExecuteOtherActors();
		}
	}
}