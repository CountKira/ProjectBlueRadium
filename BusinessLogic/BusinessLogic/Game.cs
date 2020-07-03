using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Game : IGame
	{
		readonly IRoomRepository roomRepository;
		readonly IWriter writer;
		Room room;
		readonly AttackHandler attackHandler;
		bool hasActed;
		readonly CommandInterpreter commandInterpreter;

		public Game(IWriter writer, IRoomRepository roomRepository, IRandom random, INotificationHandler<int>? healthPointsChanged = null)
		{
			this.writer = writer;
			this.roomRepository = roomRepository;
			attackHandler = new AttackHandler(writer, random, this);
			commandInterpreter = new CommandInterpreter(writer, this);
			room = roomRepository.GetStartRoom();

			Player = BusinessLogic.Player.GetNewInstance(healthPointsChanged);
		}

		public Creature Player { get; }

		public bool IsRunning { get; private set; } = true;

		void AddToPlayerInventory(Item item) => Player.Inventory.Add(item);

		/// <inheritdoc />
		public void Stop() => IsRunning = false;

		public void PickUpItem(Item item)
		{
			writer.Write(new OutputData(OutputDataType.Get) { Specifier = item.Name });
			room.RemoveItem(item);
			AddToPlayerInventory(item);
			HasActed();
		}

		public bool TryGetPortal(string portalName, out Portal portal)
			=> room.TryGetPortal(portalName, out portal);

		public void GoToRoomById(int roomId)
		{
			room = roomRepository.GetRoomById(roomId);
			writer.WriteSeenObjects(room.GetDescription());
		}

		void ExecuteOtherActors()
		{
			var creatures = room.GetCreatures();
			foreach (var creature in creatures.Where(c => !c.IsDead))
				attackHandler.GetAttackedByCreature(Player, creature);
		}

		public void HandleAttacking(string enemy)
		{
			var creature = room.GetCreature(enemy);
			if (creature is null || creature.IsDead)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
				{ Specifier = enemy });
				return;
			}

			attackHandler.AttackCreature(Player, creature);
			HasActed();
		}

		/// <inheritdoc />
		public void HasActed() => hasActed = true;

		public string? GetLocalAvailableEntityDescription(string entityName)
		{
			if (GetItemObjectInRoom(entityName) is { } floorItem)
				return floorItem.Description;

			if (Player.Inventory.GetItem(entityName) is { } playerItem)
				return playerItem.Description;

			var creature = room.GetCreature(entityName);
			return creature?.Description;
		}

		/// <inheritdoc />
		public Item? GetItemFromPlayerEquipment(string itemName) => Player.Equipment.GetItem(itemName);

		/// <inheritdoc />
		public Room GetCurrentRoom() => room;

		/// <inheritdoc />
		public Item? GetItemFromPlayerInventory(string itemName) => Player.Inventory.GetItem(itemName);

		/// <inheritdoc />
		public void WieldWeapon(Item item)
		{
			if (Player.Equipment.Any(i => i.HasTag<WeaponTag>()))
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.AlreadyWielding));
			}
			else
			{
				writer.Write(new OutputData(OutputDataType.Wield) { Specifier = item.Name });
				Player.Equipment.Add(item);
			}
		}

		/// <inheritdoc />
		public void UnwieldWeapon(Item item)
		{
			if (Player.Equipment.Contains(item))
			{
				Player.Equipment.Remove(item);
				writer.Write(new OutputData(OutputDataType.Unwield) { Specifier = item.Name });
			}

		}

		/// <inheritdoc />
		public void LearnSpell()
		{
			Player.HasSpell = true;
			HasActed();
		}

		public Item? GetItemObjectInRoom(string itemName) => room.GetItem(itemName);

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
}