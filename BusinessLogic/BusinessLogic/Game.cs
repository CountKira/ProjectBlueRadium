using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Tags;
using BusinessLogic.Verbs;

namespace BusinessLogic
{
	public class Game : IGame
	{
		readonly IRoomRepository roomRepository;
		readonly Dictionary<string, Verb> verbList;
		readonly IWriter writer;
		Room room;
		readonly AttackHandler attackHandler;
		bool spellKnown;
		bool hasActed;

		public Game(IWriter writer, IRoomRepository roomRepository, IRandom random, INotificationHandler<int>? healthPointsChanged = null)
		{
			this.writer = writer;
			this.roomRepository = roomRepository;
			attackHandler = new AttackHandler(writer, random, this);
			room = roomRepository.GetStartRoom();

			verbList = new Dictionary<string, Verb>
			{
				{"get ", new GetVerb(writer, this)},
				{"go ", new GoVerb(writer, this)},
				{"look ", new LookVerb(writer,this)},
				{"wield ", new WieldVerb(writer,this)},
				{"unwield ", new UnwieldVerb(writer, this)},
				{"read ", new ReadVerb(writer,this)},
				{"drink ", new DrinkVerb(writer, this)},
				{"attack ", new AttackVerb(writer, this)},
				{"unlock ", new UnlockVerb(writer, this)},
			};

			Player = BusinessLogic.Player.GetNewInstance(healthPointsChanged);
		}

		public Creature Player { get; }

		public bool IsRunning { get; private set; } = true;

		void AddToPlayerInventory(Item item) => Player.Inventory.Add(item);

		/// <inheritdoc />
		public void Stop() => IsRunning = false;

		void WriteAction(ActionDTO actionDto) => writer.WriteAction(actionDto);

		public void PickUpItem(Item item)
		{
			WriteAction(new ActionDTO(VerbEnum.Get) { Specifier = item.Name });
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
				writer.WriteAction(new ActionDTO(VerbEnum.Wield) { Specifier = item.Name });
				Player.Equipment.Add(item);
			}
		}

		/// <inheritdoc />
		public void UnwieldWeapon(Item item)
		{
			if (Player.Equipment.Contains(item))
			{
				Player.Equipment.Remove(item);
				writer.WriteAction(new ActionDTO(VerbEnum.Unwield) { Specifier = item.Name });
			}

		}

		/// <inheritdoc />
		public void LearnSpell()
		{
			spellKnown = true;
			HasActed();
		}

		public Item? GetItemObjectInRoom(string itemName) => room.GetItem(itemName);

		public void EnterCommand(string text)
		{
			var inputText = text.ToLower();
			switch (inputText)
			{
				case "exit":
					IsRunning = false;
					break;
				case "look":
					writer.WriteSeenObjects(room.GetDescription());
					break;
				case "spells":
					writer.DisplaySpells(spellKnown);
					break;
				case "me":
					writer.DescribeSelf(Player.Description);
					break;
				case "inventory":
					writer.ShowInventory(Player.Inventory);
					break;
				case "equipment":
					writer.ShowEquipment(Player.Equipment);
					break;
				default:
					if (!TryParseCommand(text))
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.UnknownCommand));
					break;
			}

			if (hasActed)
			{
				hasActed = false;
				ExecuteOtherActors();
			}
		}

		bool TryParseCommand(string inputText)
		{
			return verbList.Any(verb => ParseCommand(inputText, verb.Key, verb.Value.Execute));
		}

		static bool ParseCommand(string inputText, string verb, Action<string> commandMethod)
		{
			if (inputText.StartsWith(verb))
			{
				var itemName = inputText.Substring(verb.Length);
				commandMethod(itemName);
				return true;
			}

			return false;
		}
	}
}