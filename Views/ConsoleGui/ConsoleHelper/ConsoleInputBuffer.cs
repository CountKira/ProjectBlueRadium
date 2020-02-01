namespace Views.ConsoleGui.ConsoleHelper
{
	class ConsoleInputBuffer
	{
		readonly char[] input = new char[200];
		public int Counter { get; private set; }

		public void Add(char c) => input[Counter++] = c;

		/// <inheritdoc />
		public override string ToString() => new string(input, 0, Counter);

		public void RemoveLast()
		{
			if (Counter > 0)
				Counter--;
		}

		public void Clear() => Counter = 0;

		public void Write(string command)
		{
			command.CopyTo(0, input, 0, command.Length);
			Counter = command.Length;
		}
	}
}