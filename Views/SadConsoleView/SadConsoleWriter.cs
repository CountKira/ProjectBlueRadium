using System;
using System.Linq;
using BusinessLogic;
using Views.ConsoleGui;
using Console = SadConsole.Console;

namespace SadConsoleView
{
	class SadConsoleWriter : IViewWriter
	{
		readonly Console console;
		readonly int width;
		readonly int height;

		public SadConsoleWriter(Console console)
		{
			this.console = console;
			width = console.Width;
			height = console.Height;
		}

		public void WriteLine(string text)
		{
			var rows = text.Length / width;
			var overflow = Math.Max(0, console.Cursor.Row + rows - height + 1);
			console.ShiftUp(overflow);
			console.Cursor.Row -= overflow;

			console.Print(0, console.Cursor.Row, text);
			console.Cursor.Row += 2 + rows;
		}
	}
}