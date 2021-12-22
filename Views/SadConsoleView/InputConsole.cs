﻿using Microsoft.Xna.Framework.Input;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;
using SharedViewResources;

namespace SadConsoleView;

class InputConsole : ControlsConsole
{
	readonly LastCommandManager lastCommandManager = new();
	readonly Action<string> onTextEntered;
	readonly IViewWriter writer;

	public InputConsole(int width, Action<string> onTextEntered, IViewWriter writer) : base(width, 1)
	{
		this.onTextEntered = onTextEntered;
		this.writer = writer;
		Library.Default.SetControlTheme(typeof(EditableTextBox), Library.Default.GetControlTheme(typeof(TextBox)));
		var textBox = new EditableTextBox(width)
		{
			Position = new(0, 0),
		};
		textBox.KeyPressed += TextBoxOnKeyPressed;
		Add(textBox);
		FocusedControl = textBox;
	}


	void TextBoxOnKeyPressed(object? sender, TextBox.KeyPressEventArgs e)
	{
		if (sender is not EditableTextBox textBox)
			return;
		// ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
		switch (e.Key.Key)
		{
			case Keys.Enter:
			{
				var text = textBox.EditingText;
				lastCommandManager.Add(text);
				writer.WriteCommand(text);
				onTextEntered(text);
				textBox.Clear();
				e.IsCancelled = true;
				break;
			}
			case Keys.Up:
			{
				var text = lastCommandManager.GetPrevious();
				if (text != null) textBox.SetText(text);

				e.IsCancelled = true;
				break;
			}
			case Keys.Down:
			{
				var text = lastCommandManager.GetNext();
				if (text != null) textBox.SetText(text);

				e.IsCancelled = true;
				break;
			}
		}
	}
}