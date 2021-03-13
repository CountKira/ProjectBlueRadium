using BusinessLogic;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsoleView.Writer;
using SharedViewResources;
using Game = BusinessLogic.Game;

namespace SadConsoleView
{
	public class MainScreen : ContainerConsole
	{
		readonly Game game;
		readonly InputConsole textBox;

		public MainScreen()
		{
			var consoleWidth = (int) (Global.RenderWidth / (Global.FontDefault.Size.X * 1.0));
			var consoleHeight = (int) (Global.RenderHeight / (Global.FontDefault.Size.Y * 1.0));
			var statusConsole = new Console(consoleWidth, 1)
			{
				DefaultBackground = Color.Chocolate,
			};

			MainConsole = new(consoleWidth, consoleHeight - 2)
			{
				IsVisible = true,
				Position = new(0, 1),
			};
			var sadConsoleWriter = new SadConsoleWriter(MainConsole);
			var player = CreatePlayer(statusConsole);
			game = new(new ViewWriter(sadConsoleWriter), new ConsoleTestRoom(), new SystemRandom(), player);
			textBox = new(consoleWidth, game.EnterCommand, sadConsoleWriter) {Position = new(0, consoleHeight - 1),};

			Global.FocusedConsoles.Set(textBox);
			Children.Add(MainConsole);
			Children.Add(textBox);
			Children.Add(statusConsole);

			game.EnterCommand("look");
		}

		Console MainConsole { get; }
		public bool IsRunning => game.IsRunning;

		static Creature CreatePlayer(Console statusConsole)
		{
			var sword = ItemFactory.Sword();
			var inventory = new[] {sword, ItemFactory.HealingPotion(),};
			var player = new Creature("The player", "The hero of our story",
				new(10, new HealthPointWriter(statusConsole)), new(2), inventory);
			player.Equip(sword);
			return player;
		}

		public void ShutDown()
		{
			textBox.UseKeyboard = false;
		}
	}
}