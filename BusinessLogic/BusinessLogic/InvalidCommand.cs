namespace BusinessLogic
{
	public class InvalidCommand
	{
		public InvalidCommand(InvalidCommandType invalidCommandType)
		{
			CommandType = invalidCommandType;
		}

		public InvalidCommandType CommandType { get; }

		public string? Specifier { get; set; }
	}
}