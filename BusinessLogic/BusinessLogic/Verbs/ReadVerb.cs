namespace BusinessLogic.Verbs
{
	class ReadVerb : Verb
	{
		/// <inheritdoc />
		public ReadVerb(IWriter writer, IGame game) : base(writer, game) { }

		/// <inheritdoc />
		public override void Execute(string itemName)
		{
			if (Game.GetItemObjectInRoom(itemName) is { } item)
			{
				writer.Write(new OutputData(OutputDataType.LearnedSpell) { Specifier = item.Name });
				Game.LearnSpell();
			}
			else
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound)
				{ Specifier = itemName });
			}
		}
	}
}