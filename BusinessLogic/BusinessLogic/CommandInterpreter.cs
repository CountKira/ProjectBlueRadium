﻿using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Verbs;

namespace BusinessLogic
{
	class CommandInterpreter
	{
		readonly IWriter writer;
		readonly IGame game;
		readonly Dictionary<string, Verb> verbList;

		public CommandInterpreter(IWriter writer, IGame game)
		{
			this.writer = writer;
			this.game = game;
			verbList = new Dictionary<string, Verb>
			{
				{"get ", new GetVerb(writer, game)},
				{"go ", new GoVerb(writer, game)},
				{"look ", new LookVerb(writer,game)},
				{"wield ", new WieldVerb(writer,game)},
				{"unwield ", new UnwieldVerb(writer, game)},
				{"read ", new ReadVerb(writer,game)},
				{"drink ", new DrinkVerb(writer, game)},
				{"attack ", new AttackVerb(writer, game)},
				{"unlock ", new UnlockVerb(writer, game)},
			};
		}
		public void Interpret(string text)
		{
			var inputText = text.ToLower();
			switch (inputText)
			{
				case "exit":
					game.Stop();
					break;
				case "look":
					writer.WriteSeenObjects(game.GetCurrentRoom().GetDescription());
					break;
				case "spells":
					writer.DisplaySpells(game.Player.HasSpell);
					break;
				case "me":
					writer.DescribeSelf(game.Player.Description);
					break;
				case "inventory":
					writer.ShowInventory(game.Player.Inventory);
					break;
				case "equipment":
					writer.ShowEquipment(game.Player.Equipment);
					break;
				default:
					if (!TryParseCommand(text))
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.UnknownCommand));
					break;
			}
		}

		bool TryParseCommand(string inputText)
		{
			return verbList.Any(verb => ParseCommand(inputText, verb.Key, verb.Value.Execute));
		}

		static bool ParseCommand(string inputText, string verb, Action<string> commandMethod)
		{
			var match = inputText.StartsWith(verb);
			if (match)
			{
				var itemName = inputText.Substring(verb.Length);
				commandMethod(itemName);
			}

			return match;
		}
	}
}