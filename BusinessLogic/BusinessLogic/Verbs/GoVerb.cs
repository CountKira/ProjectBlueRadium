namespace BusinessLogic.Verbs
{
	class GoVerb : Verb
	{
		public GoVerb(IWriter writer) : base(writer)
		{
		}

		public override void Execute(string passageName)
		{
			if (game.TryGetConnectedRoom(passageName, out var roomId))
			{
				game.GoToRoomById(roomId);
			}
			else
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.PassageNotFound)
					{ Specifier = passageName });
			}
		}
	}
}