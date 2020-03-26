using System;
using BusinessLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Input;
using SadConsole.Themes;
using SharedViewResources;
using Game = BusinessLogic.Game;
using Console = SadConsole.Console;

namespace SadConsoleView
{
	public class MainScreen : ContainerConsole
	{
		readonly Game game;
		Console MainConsole { get; }

		public MainScreen()
		{
			var consoleWidth = (int)(Global.RenderWidth / (Global.FontDefault.Size.X * 1.0));
			var consoleHeight = (int)(Global.RenderHeight / (Global.FontDefault.Size.Y * 1.0));
			var statusConsole = new Console(consoleWidth, 1)
			{
				DefaultBackground = Color.Chocolate
			};

			MainConsole = new Console(consoleWidth, consoleHeight - 2)
			{
				IsVisible = true,
				Position = new Point(0, 1)
			};
			var statWriter = new StatWriter(statusConsole);
			game = new Game(new ViewWriter(new SadConsoleWriter(MainConsole)), new ConsoleTestRoom(), new SystemRandom(), statWriter);
			var textBox = new InputConsole(consoleWidth, game.EnterCommand) { Position = new Point(0, consoleHeight - 1) };

			Global.FocusedConsoles.Set(textBox);
			Children.Add(MainConsole);
			Children.Add(textBox);
			Children.Add(statusConsole);

			game.EnterCommand("look");
		}

	}

	public class StatWriter : INotificationHandler<Creature, int>
	{
		readonly Console statusConsole;

		public StatWriter(Console statusConsole)
		{
			this.statusConsole = statusConsole;
		}

		/// <inheritdoc />
		public void Notify(Creature sender, int e)
		{
			statusConsole.Clear();
			statusConsole.Print(0, 0, $"Hp: {e}");
		}
	}

	class InputConsole : ControlsConsole
	{
		readonly LastCommandManager lastCommandManager = new LastCommandManager();
		readonly Action<string> onTextEntered;

		public InputConsole(int width, Action<string> onTextEntered) : base(width, 1)
		{
			this.onTextEntered = onTextEntered;
			Library.Default.SetControlTheme(typeof(MyTextBox), Library.Default.GetControlTheme(typeof(TextBox)));
			var textBox = new MyTextBox(width)
			{
				Position = new Point(0, 0)
			};
			textBox.KeyPressed += TextBoxOnKeyPressed;
			Add(textBox);
			FocusedControl = textBox;
		}


		void TextBoxOnKeyPressed(object? sender, TextBox.KeyPressEventArgs e)
		{
			if (sender is MyTextBox textBox)
				switch (e.Key.Key)
				{
					case Keys.Enter:
						{
							var text = textBox.EditingText;
							lastCommandManager.Add(text);
							onTextEntered(text);
							textBox.Clear();
							e.IsCancelled = true;
							break;
						}
					case Keys.Up:
						{
							var text = lastCommandManager.GetPrevious();
							if (text != null)
							{
								textBox.SetText(text);
							}

							e.IsCancelled = true;
							break;
						}
					case Keys.Down:
						{
							var text = lastCommandManager.GetNext();
							if (text != null)
							{
								textBox.SetText(text);
							}

							e.IsCancelled = true;
							break;
						}
				}
		}
	}

	class MyTextBox : TextBox
	{
		/// <inheritdoc />
		public MyTextBox(int width) : base(width) { }

		public void Clear() => EditingText = string.Empty;

		public void SetText(string text)
		{
			EditingText = text;
			CaretPosition = text.Length;
		}
	}
}