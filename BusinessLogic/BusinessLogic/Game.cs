using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Verbs;

namespace BusinessLogic
{
	public class Game : IGame
	{
		readonly IRoomRepository roomRepository;

		readonly Dictionary<string, Verb> verbList;
		readonly IWriter writer;
		Room room;
		bool spellKnown;
		bool hasActed;

		public Game(IWriter writer, IRoomRepository roomRepository)
		{
			this.writer = writer;
			this.roomRepository = roomRepository;
			room = roomRepository.GetStartRoom();
			verbList = new Dictionary<string, Verb>
			{
				{"get ", new GetVerb(writer)},
				{"go ", new GoVerb(writer)},
				{"look ", new LookVerb(writer)},
				{"equip ", new EquipVerb(writer)},
				{"read ", new ReadVerb(writer)},
				{"drink ", new DrinkVerb(writer)},
				{"attack ", new AttackVerb(writer)},
			};
			foreach (var verb in verbList)
				verb.Value.Initialize(this);
		}

		public Player Player { get; } = new Player();

		public bool IsRunning { get; private set; } = true;

		/// <inheritdoc />
		public void AddToPlayerInventory(Item item)
		{
			Player.Inventory.Add(item);
		}

		/// <inheritdoc />
		public void Stop() => IsRunning = false;

		public void WriteAction(ActionDTO actionDto) => writer.WriteAction(actionDto);

		public void PickUpItem(Item item)
		{
			WriteAction(new ActionDTO(VerbEnum.Get) { Specifier = item.Name });
			room.RemoveItem(item);
			AddToPlayerInventory(item);
		}

		public bool TryGetConnectedRoom(string passageName, out int roomId)
			=> room.TryGetRoom(passageName, out roomId);

		public void GoToRoomById(int roomId)
		{
			room = roomRepository.GetRoomById(roomId);
			writer.WriteSeenObjects(room.GetDescription());
		}

		void ExecuteOtherActors()
		{
			var creatures = room.GetCreatures();
			foreach (var creature in creatures.Where(c => c.HealthPoints > 0))
				GetAttackedByCreature(creature);
		}

		public void HandleAttackingTheEvilGuy(string enemy)
		{
			var creature = room.GetCreature(enemy);
			if (creature is null)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
				{ Specifier = enemy });
				return;
			}

			AttackCreature(creature);
		}

		/// <inheritdoc />
		public bool HasActed() => hasActed = true;

		public object GetLocalAvailableEntity(string entityName)
		{
			var item = GetItemObjectInRoom(entityName);
			if (item != null) return item;
			if (Player.Inventory.HasItem(entityName)) return Player.Inventory.First(i => i.Name == entityName);

			var creature = room.GetCreature(entityName);
			return creature;
		}

		/// <inheritdoc />
		public Item GetItemFromPlayerInventory(string itemName)
			=> Player.Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

		/// <inheritdoc />
		public void EquipWeapon(Item item)
		{
			if (Player.Equipment.HasItem(item.Name))
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.AlreadyEquipped)
				{ Specifier = item.Name });
			}
			else
			{
				writer.WriteAction(new ActionDTO(VerbEnum.Equip) { Specifier = item.Name });
				Player.Equipment.Add(item);
			}
		}

		/// <inheritdoc />
		public void LearnSpell()
		{
			spellKnown = true;
		}

		public Item? GetItemObjectInRoom(string itemName) => room.HasItem(itemName) ? room.GetItem(itemName) : null;

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
					writer.DescribeSelf("It is you.");
					break;
				case "inventory":
					writer.ShowInventory(Player.Inventory);
					break;
				case "equipment":
					writer.ShowEquipment(Player.Equipment);
					break;
				case "hp":
					writer.ShowHealthPoints(Player.HitPoints);
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

		void GetAttackedByCreature(Creature creature)
		{
			var damage = creature.Damage;
			writer.WriteTextOutput($"The {creature.Name} attacks you and deals {damage} damage.");
			Player.HitPoints -= damage;

			if (Player.IsDead())
			{
				writer.WriteTextOutput($"The {creature.Name} killed you.");
				IsRunning = false;
			}
		}

		void AttackCreature(Creature creature)
		{
			DealDamageToCreature(Player.Equipment.HasItem("sword") ? 2 : 1, creature);
			if (creature.HealthPoints <= 0)
			{
				writer.WriteTextOutput("You have slain the enemy. A winner is you.");
				IsRunning = false;
			}
		}

		void DealDamageToCreature(int damage, Creature creature)
		{
			creature.HealthPoints -= damage;
			writer.WriteTextOutput($"You attack the {creature.Name} and deal {damage} damage.");
		}

		bool TryParseCommand(string inputText)
		{
			foreach (var verb in verbList)
				if (ParseCommand(inputText, verb.Key, verb.Value.Execute))
					return true;

			return false;
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