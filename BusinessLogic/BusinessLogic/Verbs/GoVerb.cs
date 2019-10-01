namespace BusinessLogic.Verbs
{
	class GoVerb : Verb
	{
		public GoVerb(IWriter writer) : base(writer) { }

		public override void Execute(string passageName)
		{
			if (Game!.TryGetConnectedRoom(passageName, out var roomId))
				Game.GoToRoomById(roomId);
			else
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.PassageNotFound)
				{ Specifier = passageName });
		}
	}
}