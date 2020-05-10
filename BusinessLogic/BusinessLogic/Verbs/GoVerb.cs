using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class GoVerb : Verb
	{
		public GoVerb(IWriter writer) : base(writer) { }

		public override void Execute(string passageName)
		{
			if (Game!.TryGetPassage(passageName, out var passage))
				if (passage.GetTag<LockTag>()?.IsLocked ?? false)
				{
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.Locked));
				}
				else
				{
					Game.GoToRoomById(passage.RoomGuid);
				}
			else
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.PassageNotFound)
				{ Specifier = passageName });
		}
	}
}