using System;

namespace Views.ConsoleGui.ConsoleHelper
{
	class ConsoleLineReader
	{
		enum CommandDirection
		{
			Next, Previous
		}

		readonly LastCommandManager commands = new LastCommandManager();
		readonly ConsoleInputBuffer buffer = new ConsoleInputBuffer();
		int startX, startY;

		public string ReadLine()
		{
			Init();
			while (true)
			{
				var key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.Enter:
						Console.WriteLine();
						var command = buffer.ToString();
						commands.Add(command);
						return buffer.ToString();
					case ConsoleKey.Backspace:
						if (IsAtStart())
							break;
						Console.Write("\b \b");
						buffer.RemoveLast();
						break;
					case ConsoleKey.Escape:
						ClearInputLine();
						break;
					case ConsoleKey.UpArrow:
					case ConsoleKey.DownArrow:
						CycleThroughLastCommands(key.Key == ConsoleKey.UpArrow ?
							CommandDirection.Previous :
							CommandDirection.Next);
						break;
					default:
						Console.Write(key.KeyChar);
						buffer.Add(key.KeyChar);
						break;
				}
			}
		}

		bool IsAtStart() => (startX == Console.CursorLeft && startY == Console.CursorTop);

		void Init()
		{
			buffer.Clear();
			startX = Console.CursorLeft;
			startY = Console.CursorTop;
		}

		void ClearInputLine()
		{
			Console.SetCursorPosition(startX, startY);
			Console.Write(new string(' ', buffer.Counter));
			Console.SetCursorPosition(startX, startY);
			buffer.Clear();
		}

		void CycleThroughLastCommands(CommandDirection direction)
		{
			var command = direction switch
			{
				CommandDirection.Next => commands.GetNext(),
				CommandDirection.Previous => commands.GetPrevious(),
				_ => throw new ArgumentException(nameof(direction))
			};
			if (command == null)
				return;
			Write(command);
		}

		void Write(string command)
		{
			ClearInputLine();
			Console.Write(command);
			buffer.Write(command);
		}
	}
}