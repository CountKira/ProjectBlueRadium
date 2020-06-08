namespace BusinessLogic.Verbs
{
	class GetVerb : Verb
	{
		public GetVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(string item)
		{
			if (Game.GetItemObjectInRoom(item) is { } itemObj)
			{
				Game.PickUpItem(itemObj);
			}
			else
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) { Specifier = item });
		}
	}
}