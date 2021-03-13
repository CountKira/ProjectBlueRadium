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
				writer.Write(new(OutputDataType.LearnedSpell) {Specifier = item.Name,});
				Game.LearnSpell();
			}
			else
			{
				writer.SetInvalidCommand(new(InvalidCommandType.ItemNotFound)
					{Specifier = itemName,});
			}
		}
	}
}