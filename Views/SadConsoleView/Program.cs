using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;

namespace SadConsoleView
{
	public static class Program
	{
		static void Main()
		{
			// Setup the engine and create the main window.
			Game.Create(80, 25);

			// Hook the start event so we can add consoles to the system.
			Game.OnInitialize = Init;

			// Start the game.
			Game.Instance.Run();
			Game.Instance.Dispose();
		}

		static void Init()
		{
			var console = new MainScreen();
			Global.CurrentScreen = console;
		}
	}
}
