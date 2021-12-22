using BusinessLogic.Tags;

namespace BusinessLogic.Verbs;

class UnlockVerb : Verb
{
	/// <inheritdoc />
	public UnlockVerb(IWriter writer, IGame game) : base(writer, game) { }

	/// <inheritdoc />
	public override void Execute(ExecutionTarget target)
	{
		if (!Game.TryGetPortal(PortalName.FromExecutionTarget(target), out var portal)) 
			return;
		var lockTag = portal!.Passage.GetTag<LockTag>();
		if (lockTag != null)
		{
			if (Game.Player.HasKey(lockTag))
			{
				writer.Write(new(OutputDataType.Unlocked) {Specifier = portal.DisplayName.Value,});
				lockTag.Unlock();
				Game.HasActed();
			}
			else
			{
				writer.SetInvalidCommand(new(InvalidCommandType.MissingKey));
			}
		}
		else
		{
			writer.SetInvalidCommand(new(InvalidCommandType.NotLocked) {Specifier = portal.DisplayName.Value,});
		}
	}
}