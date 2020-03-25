using System;
using BusinessLogic;
using SharedViewResources;
using Views.ConsoleGui.ConsoleHelper;

namespace Views.ConsoleGui
{
	static class Program
	{
		class SystemRandom : IRandom
		{
			readonly Random random = new Random();
			/// <inheritdoc />
			public int Next(int i) => random.Next(i);
		}

		static void Main()
		{
			var writer = new ConsoleWriter();
			var game = new Game(new ViewWriter(writer), new ConsoleTestRoom(), new SystemRandom());
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