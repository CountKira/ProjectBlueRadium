using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class UnlockVerb : Verb
	{
		/// <inheritdoc />
		public UnlockVerb(IWriter writer, IGame game) : base(writer, game) { }

		/// <inheritdoc />
		public override void Execute(string portalName)
		{
			if (Game.TryGetPortal(portalName, out var portal))
			{
				var lockTag = portal.Passage.GetTag<LockTag>();
				if (lockTag != null)
				{
					if (Game.Player.HasKey(lockTag))
					{
						writer.Write(new(OutputDataType.Unlocked) {Specifier = portal.DisplayName,});
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
					writer.SetInvalidCommand(new(InvalidCommandType.NotLocked) {Specifier = portal.DisplayName,});
				}
			}
		}
	}
}