using SharedViewResources;
using Console = SadConsole.Console;

namespace SadConsoleView.Writer;

class SadConsoleWriter : IViewWriter
{
	readonly Console console;
	readonly int height;
	readonly int width;

	public SadConsoleWriter(Console console)
	{
		this.console = console;
		width = console.Width;
		height = console.Height;
	}

	public void WriteLine(string text)
	{
		var rows = (text.Length - 1) / width + 1;
		var overflow = Math.Max(0, console.Cursor.Row + rows - height);
		console.ShiftUp(overflow);
		console.Cursor.Row -= overflow;
		console.Cursor.Column = 0;
		console.Cursor.Print(text);
		console.Cursor.Row += 2;
	}

	public void WriteCommand(string text)
	{
		text = $"> {text}";
		if (text.Length > width)
		{
			var p1 = text.Substring(0, width);
			var i = p1.LastIndexOf(' ');
			text = i < 2
				? $"{p1}{Environment.NewLine}{text.Substring(width)}"
				: $"{text.Substring(0, i)}{Environment.NewLine}{text.Substring(i)}";
		}

		WriteLine(text);
	}
}