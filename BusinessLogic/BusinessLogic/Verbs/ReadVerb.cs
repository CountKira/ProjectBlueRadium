namespace BusinessLogic.Verbs
{
	class ReadVerb : Verb
	{
		/// <inheritdoc />
		public ReadVerb(IWriter writer, IGame game) : base(writer, game) { }

		/// <inheritdoc />
		public override void Execute(string item)
		{
			if (Game.GetItemObjectInRoom(item) is { } obj)
			{
				writer.LearnedFireball();
				Game.LearnSpell();
			}
			else
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound)
				{ Specifier = item });
			}
		}
	}
}