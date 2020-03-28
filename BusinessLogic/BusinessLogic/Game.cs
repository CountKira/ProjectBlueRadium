using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Tags;
using BusinessLogic.Verbs;

namespace BusinessLogic
{
	public class Game : IGame
	{
		readonly IRoomRepository roomRepository;
		readonly IRandom random;
		readonly Dictionary<string, Verb> verbList;
		readonly IWriter writer;
		Room room;
		bool spellKnown;
		bool hasActed;

		public Game(IWriter writer, IRoomRepository roomRepository, IRandom random, INotificationHandler<Creature, int>? healthPointsChanged = null)
		{
			this.writer = writer;
			this.roomRepository = roomRepository;
			this.random = random;
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
			Player = new Player(healthPointsChanged);
			healthPointsChanged?.Notify(Player, Player.HealthPoints);
		}

		public Player Player { get; }

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

		public void HandleAttacking(string enemy)
		{
			var creature = room.GetCreature(enemy);
			if (creature is null)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
				{ Specifier = enemy });
				return;
			}

			if (creature.HealthPoints <= 0)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
				{ Specifier = enemy });
				return;
			}
			AttackCreature(creature);
			HasActed();
		}

		/// <inheritdoc />
		public void HasActed() => hasActed = true;

		public object? GetLocalAvailableEntity(string entityName)
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

		void GetAttackedByCreature(Creature creature)
		{
			if (DoesMiss())
			{
				writer.WriteTextOutput($"The {creature.Name} missed his attack.");
				return;
			}
			var damage = creature.Damage;
			writer.WriteTextOutput($"The {creature.Name} attacks you and deals {damage} damage.");
			Player.HealthPoints -= damage;

			if (Player.IsDead())
			{
				writer.WriteTextOutput($"The {creature.Name} killed you.");
				IsRunning = false;
			}
		}

		void AttackCreature(Creature creature)
		{
			if (DoesMiss())
			{
				writer.WriteTextOutput("Missed.");
				return;
			}

			var weapon = Player.Equipment.FirstOrDefault(i => i.HasTag(Tag.Weapon));
			var damage = weapon?.GetTag<WeaponTag>(Tag.Weapon)?.Damage ?? 1;
			DealDamageToCreature(damage, creature);
			if (creature.HealthPoints <= 0)
			{
				writer.WriteTextOutput($"You have slain the {creature.Name}.");

				if (creature.HasTag(Tag.GameEnd))
				{
					writer.WriteTextOutput("You have slain the final enemy. A winner is you.");
					IsRunning = false;
				}
			}
		}

		bool DoesMiss() => random.Next(2) == 0;

		void DealDamageToCreature(int damage, Creature creature)
		{
			creature.HealthPoints -= damage;
			writer.WriteTextOutput($"You attack the {creature.Name} and deal {damage} damage.");
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