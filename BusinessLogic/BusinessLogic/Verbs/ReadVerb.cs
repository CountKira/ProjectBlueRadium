namespace BusinessLogic.Verbs
{
	class ReadVerb : Verb
	{
		/// <inheritdoc />
		public ReadVerb(IWriter writer) : base(writer) { }

		/// <inheritdoc />
		public override void Execute(string item)
		{
			var obj = Game!.GetItemObjectInRoom(item);
			if (obj != null)
			{
				writer.LearnedFireball();
				Game.LearnSpell();
			}
			else
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound)
					{Specifier = item});
			}
		}
	}
}