namespace BusinessLogic.Verbs
{
	class GetVerb : Verb
	{
		public GetVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(ExecutionTarget target)
		{
			if (Game.GetItemObjectInRoom(ItemName.FromExecutionTarget(target)) is { } itemObj)
				Game.PickUpItem(itemObj);
			else
				writer.SetInvalidCommand(new(InvalidCommandType.ItemNotFound) {Specifier = target.Value,});
		}
	}
}