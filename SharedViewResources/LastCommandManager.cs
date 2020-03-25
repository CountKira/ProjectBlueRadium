using System;
using System.Collections.Generic;

namespace SharedViewResources
{
	public class LastCommandManager
	{
		readonly LinkedList<string> commands = new LinkedList<string>();
		LinkedListNode<string>? currentCommand;

		public LastCommandManager()
		{
			commands.AddLast(string.Empty);
			currentCommand = commands.Last;
		}
		public void Add(string command)
		{
			if (command == null)
			{
				throw new ArgumentException(nameof(command));
			}
			if (command != string.Empty)
			{
				var node = commands.Find(command);
				if (node != null)
					commands.Remove(node);

				commands.AddBefore(commands.Last!, command);
			}
			currentCommand = commands.Last;
		}

		public string? GetPrevious()
		{
			if (currentCommand == null)
			{
				throw new InvalidOperationException();
			}
			if (currentCommand == commands.First)
			{
				return null;
			}
			currentCommand = currentCommand.Previous;
			return currentCommand?.Value;
		}

		public string? GetNext()
		{
			if (currentCommand == null || currentCommand == commands.Last || currentCommand.Next == commands.Last)
				return null;
			currentCommand = currentCommand.Next;
			return currentCommand?.Value;
		}
	}
}