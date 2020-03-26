using System;
using SadConsole.Controls;

namespace SadConsoleView {
	class EditableTextBox : TextBox
	{
		/// <inheritdoc />
		public EditableTextBox(int width) : base(width) { }

		public void Clear() => EditingText = string.Empty;

		public void SetText(string text)
		{
			EditingText = text;
			CaretPosition = text.Length;
			LeftDrawOffset = Math.Max(0, CaretPosition - Width + 1);
		}
	}
}