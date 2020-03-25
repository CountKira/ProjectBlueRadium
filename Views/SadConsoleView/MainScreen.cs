using System;
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
		readonly LastCommandManager lastCommandManager = new LastCommandManager();
		Console MainConsole { get; }

		public MainScreen()
		{

			var consoleWidth = (int)(Global.RenderWidth / (Global.FontDefault.Size.X * 1.0));
			var consoleHeight = (int)(Global.RenderHeight / (Global.FontDefault.Size.Y * 1.0));

			MainConsole = new Console(consoleWidth, consoleHeight - 1)
			{
				IsVisible = true,
			};
			game = new Game(new ViewWriter(new SadConsoleWriter(MainConsole)), new ConsoleTestRoom(), new SystemRandom());

			var textBox = new InputConsole(consoleWidth, 1) { Position = new Point(0, consoleHeight - 1) };
			textBox.KeyPressed += TextBoxOnKeyPressed;

			Global.FocusedConsoles.Set(textBox);
			Children.Add(MainConsole);
			Children.Add(textBox);
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
							game.EnterCommand(text);
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

	class InputConsole : ControlsConsole
	{
		readonly MyTextBox textBox;
		public InputConsole(int width, int height) : base(width, height)
		{
			Library.Default.SetControlTheme(typeof(MyTextBox), Library.Default.GetControlTheme(typeof(TextBox)));
			textBox = new MyTextBox(width)
			{
				Position = new Point(0, 0)
			};

			Add(textBox);
			FocusedControl = textBox;
		}

		public event EventHandler<TextBox.KeyPressEventArgs> KeyPressed
		{
			add => textBox.KeyPressed += value;
			remove => textBox.KeyPressed -= value;
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