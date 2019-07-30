﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
	public class Game : IGame
	{
		private readonly IRoomRepository roomRepository;
		private readonly IWriter writer;
		private Room room;
		private bool spellKnown;
		private int hp = 4;

		public Game(IWriter writer, IRoomRepository roomRepository)
		{
			this.writer = writer;
			this.roomRepository = roomRepository;
			room = roomRepository.GetStartRoom();
		}

		public Player Player { get; } = new Player();

		public bool IsRunning { get; private set; } = true;

		/// <inheritdoc />
		public void AddToPlayerInventory(Item item)
		{
			Player.Inventory.Add(item);
		}

		public void YouDiedByPoison() => writer.YouDiedByPoison();

		/// <inheritdoc />
		public void Stop() => IsRunning = false;

		public void WriteAction(ActionDTO actionDto) => writer.WriteAction(actionDto);

		private void PickUpItem(Item item) => room.PickUpItem(item, this);


		private void WriteDescription(string text) => writer.WriteTextOutput(text);

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
				case "drink bottle":
					if (GetLocalAvailableEntity("bottle") is Item entityObj)
					{
						entityObj.Act(Verb.Drink, this);
					}
					else
					{
						var invalidCommand = new InvalidCommand(InvalidCommandType.ItemNotFound)
						{
							Specifier = "bottle"
						};
						writer.SetInvalidCommand(invalidCommand);
					}

					break;
				case "read fireball spell book":
					if (room.HasItem("fireball spell book"))
					{
						writer.LearnedFireball();
						spellKnown = true;
					}
					else
					{
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound)
						{ Specifier = "fireball spell book" });
					}

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
				case "attack evil guy":
					HandleAttackingTheEvilGuy();
					break;
				case "hp":
					writer.ShowHealthPoints(hp);
					break;
				default:
					if (!TryParseCommand(text))
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.UnknownCommand));
					break;
			}
		}

		private void HandleAttackingTheEvilGuy()
		{
			var creature = room.GetCreature("evil guy");
			if (creature is null)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
				{ Specifier = "evil guy" });
				return;
			}

			AttackCreature(creature);
			if (!IsRunning)
			{
				return;
			}
			GetAttackedByCreature(creature);
		}

		private void GetAttackedByCreature(Creature creature)
		{
			writer.WriteTextOutput($"The {creature.Name} attacks you and deals 2 damage.");
			hp -= 2;

			if (hp <= 0)
			{
				writer.WriteTextOutput($"The {creature.Name} killed you.");
				IsRunning = false;
			}
		}

		private void AttackCreature(Creature creature)
		{
			DealDamageToCreature(Player.Equipment.HasItem("sword") ? 2 : 1, creature);
			if (creature.HealthPoints <= 0)
			{
				writer.WriteTextOutput("You have slain the enemy. A winner is you.");
				IsRunning = false;
			}
		}

		private void DealDamageToCreature(int damage, Creature creature)
		{
			creature.HealthPoints -= damage;
			writer.WriteTextOutput($"You attack the {creature.Name} and deal {damage} damage.");
		}

		private object GetLocalAvailableEntity(string entityName)
		{
			var item = GetItemObjectInRoom(entityName);
			if (item != null) return item;
			if (Player.Inventory.HasItem(entityName)) return Player.Inventory.First(i => i.Name == entityName);

			var creature = room.GetCreature(entityName);
			return creature;
		}

		private Item GetItemObjectInRoom(string itemName) => room.HasItem(itemName) ? room.GetItem(itemName) : null;

		private bool TryParseCommand(string inputText) =>
			ParseGetCommand(inputText) ||
			ParseGoCommand(inputText) ||
			TryParseLookCommand(inputText) ||
			TryParseEquipCommand(inputText);

		private bool TryParseEquipCommand(string inputText)
		{
			if (inputText.StartsWith("equip "))
			{
				var itemName = inputText.Substring(6);
				var itemObj =
					Player.Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
				if (itemObj is null)
				{
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
					{ Specifier = itemName });
				}
				else
				{
					if (itemObj.HasTag("weapon"))
					{
						if (Player.Equipment.HasItem(itemObj.Name))
						{
							writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.AlreadyEquipped)
							{ Specifier = itemObj.Name });
						}
						else
						{
							writer.WriteAction(new ActionDTO(Verb.Equip) { Specifier = itemObj.Name });
							Player.Equipment.Add(itemObj);
						}
					}
					else
					{
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.CanNotEquip)
						{ Specifier = itemObj.Name });
					}
				}

				return true;
			}

			return false;
		}

		private bool ParseGetCommand(string inputText)
		{
			if (inputText.StartsWith("get "))
			{
				var item = inputText.Substring(4);
				var itemObj = GetItemObjectInRoom(item);
				if (itemObj is null)
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) { Specifier = item });
				else
					PickUpItem(itemObj);

				return true;
			}

			return false;
		}

		private bool ParseGoCommand(string inputText)
		{
			if (inputText.StartsWith("go "))
			{
				var passageName = inputText.Substring(3);
				if (room.TryGetRoom(passageName, out var roomId))
				{
					room = roomRepository.GetRoomById(roomId);
					writer.WriteSeenObjects(room.GetDescription());
				}
				else
				{
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.PassageNotFound)
					{ Specifier = passageName });
				}

				return true;
			}

			return false;
		}

		private bool TryParseLookCommand(string inputText)
		{
			if (inputText.StartsWith("look "))
			{
				var entity = inputText.Substring(5);
				ExecuteLookCommand(entity);

				return true;
			}

			return false;
		}

		private void ExecuteLookCommand(string entity)
		{
			var entityObj = GetLocalAvailableEntity(entity);
			switch (entityObj)
			{
				case null:
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
					{ Specifier = entity });
					break;
				case Item item:
					WriteDescription(item.Description);
					break;
				case Creature creature:
					WriteDescription(creature.Description);
					break;
			}
		}
	}
}