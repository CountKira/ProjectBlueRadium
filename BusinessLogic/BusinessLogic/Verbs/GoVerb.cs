using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class GoVerb : Verb
	{
		public GoVerb(IWriter writer) : base(writer) { }

		public override void Execute(string portalName)
		{
			if (Game!.TryGetPortal(portalName, out var portal))
				if (portal.Passage.GetTag<LockTag>()?.IsLocked ?? false)
				{
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.Locked));
				}
				else
				{
					Game.GoToRoomById(portal.RoomGuid);
				}
			else
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.PortalNotFound)
				{ Specifier = portalName });
		}
	}
}