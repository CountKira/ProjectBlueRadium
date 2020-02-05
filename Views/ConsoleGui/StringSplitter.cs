using System;
using System.Text;

namespace Views.ConsoleGui
{
	public static class StringSplitter
	{
		public static string BreakText(string text, int width)
		{
			var sb = new StringBuilder();
			var t = text;
			while (t.Length > width)
			{
				var index = FindLastSpace(t, width);
				var (first, second) = SplitString(t, index);
				sb.Append(first);
				if (first.Length < width)
				{
					sb.Append(Environment.NewLine);
				}
				t = second;
			}

			sb.Append(t);
			return sb.ToString();
		}

		static (string first, string second) SplitString(string text, int index)
		{
			var f = text.Substring(0, index);
			var s = text.Substring(index + 1, text.Length - index - 1);
			return (f, s);
		}

		static int FindLastSpace(string text, int startPosition)
		{
			for (var i = startPosition; i >= 0; i--)
				if (text[i] == ' ')
					return i;

			return -1;
		}
	}
}