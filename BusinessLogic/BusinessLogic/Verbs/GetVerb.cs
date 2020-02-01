namespace BusinessLogic.Verbs
{
	class GetVerb : Verb
	{
		public GetVerb(IWriter writer) : base(writer) { }

		public override void Execute(string item)
		{
			var itemObj = Game!.GetItemObjectInRoom(item);
			if (itemObj is null)
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.ItemNotFound) {Specifier = item});
			else
			{
				Game.PickUpItem(itemObj);
				Game.HasActed();
			}
		}
	}
}