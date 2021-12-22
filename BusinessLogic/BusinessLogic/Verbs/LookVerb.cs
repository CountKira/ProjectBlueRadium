namespace BusinessLogic.Verbs;

class LookVerb : Verb
{
	public LookVerb(IWriter writer, IGame game) : base(writer, game) { }

	public override void Execute(ExecutionTarget target)
	{
		var entityObj = Game.GetLocalAvailableEntityDescription(target.Value);
		if (entityObj == null)
			writer.SetInvalidCommand(new(InvalidCommandType.EntityNotFound)
				{Specifier = target.Value,});
		else
			writer.WriteTextOutput(entityObj);
	}
}