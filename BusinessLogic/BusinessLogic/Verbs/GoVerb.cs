using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class GoVerb : Verb
	{
		public GoVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(string portalName)
		{
			if (Game.TryGetPortal(portalName, out var portal))
				if (portal!.Passage.GetTag<LockTag>()?.IsLocked ?? false)
					writer.SetInvalidCommand(new(InvalidCommandType.Locked));
				else
					Game.GoToRoomById(portal.RoomGuid);
			else
				writer.SetInvalidCommand(new(InvalidCommandType.PortalNotFound)
					{Specifier = portalName,});
		}
	}
}