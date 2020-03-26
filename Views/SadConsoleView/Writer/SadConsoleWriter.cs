using System;
using System.Linq;
using SharedViewResources;
using Console = SadConsole.Console;

namespace SadConsoleView.Writer
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
			text = StringSplitter.BreakText(text, width);
			var rows = (text.Length - 1) / width + 1;
			var overflow = Math.Max(0, console.Cursor.Row + rows - height);
			console.ShiftUp(overflow);
			console.Cursor.Row -= overflow;
			console.Cursor.Column = 0;
			console.Cursor.Print(text);
			console.Cursor.Row += 2;
		}
	}
}