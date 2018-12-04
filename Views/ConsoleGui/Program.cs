using System;
using BusinessLogic;
using SharedTestingResources;

namespace Views.ConsoleGui
{
	static class Program
	{
		static void Main()
		{
			var writer = new ConsoleWriter();
			var game = new Game(writer, new TestingRoomRepository());
			game.EnterCommand("look");
			Console.Write(">");
			while (game.IsRunning)
			{
				var input = Console.ReadLine();
				Console.WriteLine();
				game.EnterCommand(input);
				if (game.IsRunning)
				{
					Console.Write(">");
				}
			}
			writer.WriteDescription("The only way to escape alive, is to end the game.");
		}
	}
}
