namespace BusinessLogic.Verbs
{
	class GetVerb : Verb
	{
		public GetVerb(IWriter writer) : base(writer) { }

		public override void Execute(string item)
		{
			var itemObj = game.GetItemObjectInRoom(item);
			if (itemObj is null)
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) { Specifier = item });
			else
				game.PickUpItem(itemObj);
		}
	}
}