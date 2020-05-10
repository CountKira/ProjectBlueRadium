using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class UnlockVerb : Verb
	{
		/// <inheritdoc />
		public UnlockVerb(IWriter writer) : base(writer) { }

		/// <inheritdoc />
		public override void Execute(string s)
		{
			if (Game.TryGetPassage(s, out var passage))
			{
				var lockTag = passage.GetTag<LockTag>();
				if (lockTag != null)
				{
					if (PlayerHasKey(lockTag))
					{
						writer.WriteAction(new ActionDTO(VerbEnum.Unlocked) { Specifier = passage.DisplayName });
						lockTag.Unlock();
						Game.HasActed();
					}
					else
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.MissingKey));
				}
				else
				{
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.NotLocked) { Specifier = passage.DisplayName });
				}
			}
		}

		bool PlayerHasKey(LockTag lockTag)
		{
			return Game.Player.Inventory.Any(i => i.GetTag<KeyTag>()?.LockId == lockTag.LockId);
		}
	}
}
