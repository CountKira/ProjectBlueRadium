﻿using System;
using BusinessLogic;
using SharedTestingResources;
using Views.ConsoleGui.ConsoleHelper;

namespace Views.ConsoleGui
{
	static class Program
	{
		static void Main()
		{
			var writer = new ConsoleWriter();
			var game = new Game(writer, new ConsoleTestRoom());
			var lineReader = new ConsoleLineReader();
			game.EnterCommand("look");
			Console.Write(">");
			while (game.IsRunning)
			{
				var input = lineReader.ReadLine();
				Console.WriteLine();
				game.EnterCommand(input);
				if (game.IsRunning) Console.Write(">");
			}
		}
	}
}