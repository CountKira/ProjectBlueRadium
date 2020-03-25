using System;
using System.Linq;
using BusinessLogic;

namespace Views.ConsoleGui
{
	class ConsoleWriter : IViewWriter
	{
		public void WriteLine(string text) =>
			Console.WriteLine(StringSplitter.BreakText(text, Console.WindowWidth));
	}
}