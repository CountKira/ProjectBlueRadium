﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Items;

namespace BusinessLogic
{
	public class Game
	{
		readonly IWriter writer;
		Room room;
		public ItemCollection Inventory { get; } = new ItemCollection();
		readonly IRoomRepository roomRepository;
		bool spellKnown;

		public Game(IWriter writer, IRoomRepository roomRepository)
		{
			this.writer = writer;
			this.roomRepository = roomRepository;
			room = roomRepository.Rooms[0];
			Inventory.RegisterGame(this);
			foreach (var roomRepositoryRoom in roomRepository.Rooms)
			{
				roomRepositoryRoom.RegisterGame(this);
			}
		}

		public bool IsRunning { get; set; } = true;

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
					var entityObj = GetLocalAvailableEntity("bottle") as Item;
					if (entityObj is null)
					{
						var invalidCommand = new InvalidCommand(InvalidCommandType.ItemNotFound)
						{
							Specifier = "bottle"
						};
						writer.SetInvalidCommand(invalidCommand);
					}
					else
						entityObj.Act(Verb.Drink);
					break;
				case "read fireball spell book":
					if (room.HasItem("fireball spell book"))
					{
						writer.LearnedFireball();
						spellKnown = true;
					}
					else
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) { Specifier = "fireball spell book" });

					break;
				case "spells":
					
					writer.DisplaySpells(spellKnown);
					break;
				case "me":
					writer.DescribeSelf("It is you.");
					break;
				case "inventory":
					writer.ShowInventory(Inventory);
					break;
				case "equipment":
					writer.ShowEquipment();
					break;
				default:
					if (!TryParseCommand(inputText))
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.UnknownCommand));
					break;
			}
		}

		object GetLocalAvailableEntity(string entityName)
		{
			var item = GetItemObjectInRoom(entityName);
			if (item != null)
			{
				return item;
			}
			if (Inventory.HasItem(entityName))
			{
				return Inventory.First(i => i.Name == entityName);
			}

			var creature = room.GetCreature(entityName);
			if (creature != null)
			{
				return creature;
			}


			return null;
		}

		Item GetItemObjectInRoom(string itemName)
		{
			return room.HasItem(itemName) ? room.GetItem(itemName) : null;
		}

		bool TryParseCommand(string inputText)
		{
			return ParseGetCommand(inputText) ||
					ParseGoCommand(inputText) ||
					ParseLookCommand(inputText);
		}

		bool ParseGetCommand(string inputText)
		{
			if (inputText.StartsWith("get "))
			{
				var item = inputText.Substring(4);
				var itemObj = GetItemObjectInRoom(item);
				if (itemObj is null)
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) { Specifier = item });
				else
					itemObj.Act(Verb.Get);

				return true;
			}

			return false;
		}

		bool ParseGoCommand(string inputText)
		{
			if (inputText.StartsWith("go "))
			{
				var passageName = inputText.Substring(3);
				if (room.TryGetRoom(passageName, out var roomId))
				{
					room = roomRepository.Rooms[roomId];
					writer.WriteSeenObjects(room.GetDescription());
				}
				else
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.PassageNotFound) { Specifier = passageName });

				return true;
			}

			return false;
		}

		bool ParseLookCommand(string inputText)
		{
			if (inputText.StartsWith("look "))
			{
				var entity = inputText.Substring(5);
				var entityObj = GetLocalAvailableEntity(entity);
				switch (entityObj)
				{
					case null:
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) { Specifier = entity });
						break;
					case Item item:
						item.Act(Verb.Look);
						break;
					case Creature creature:
						WriteDescription(creature.Description);
						break;
				}

				return true;
			}

			return false;
		}

		public void PickUpItem(Item item)
		{
			room.PickUpItem(item);
		}


		public void WriteDescription(string text)
		{
			writer.WriteDescription(text);
		}

		public void YouDiedByPoison()
		{
			writer.YouDiedByPoison();
		}

		public void WriteAction(ActionDTO actionDto)
		{
			writer.WriteAction(actionDto);
		}
	}
}

