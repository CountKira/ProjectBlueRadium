namespace BusinessLogic
{
	public class InvalidCommand
	{
		public InvalidCommandType CommandType { get; }

		public InvalidCommand(InvalidCommandType invalidCommandType)
		{
			CommandType = invalidCommandType;
		}

		public string Specifier { get; set; }
	}
}