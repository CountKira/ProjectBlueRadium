namespace BusinessLogic.Verbs;

class ReadVerb : Verb
{
	/// <inheritdoc />
	public ReadVerb(IWriter writer, IGame game) : base(writer, game) { }

	/// <inheritdoc />
	public override void Execute(ExecutionTarget target)
	{
		if (Game.GetItemObjectInRoom(ItemName.FromExecutionTarget(target)) is { } item)
		{
			writer.Write(new(OutputDataType.LearnedSpell) {Specifier = item.Name.Value,});
			Game.LearnSpell();
		}
		else
		{
			writer.SetInvalidCommand(new(InvalidCommandType.ItemNotFound)
				{Specifier = target.Value,});
		}
	}
}