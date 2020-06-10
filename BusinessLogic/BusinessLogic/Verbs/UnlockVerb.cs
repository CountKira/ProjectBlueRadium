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
		public UnlockVerb(IWriter writer, IGame game) : base(writer, game) { }

		/// <inheritdoc />
		public override void Execute(string portalName)
		{
			if (Game.TryGetPortal(portalName, out var portal))
			{
				var lockTag = portal.Passage.GetTag<LockTag>();
				if (lockTag != null)
				{
					if (PlayerHasKey(lockTag))
					{
						writer.Write(new OutputData(OutputDataType.Unlocked) { Specifier = portal.DisplayName });
						lockTag.Unlock();
						Game.HasActed();
					}
					else
						writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.MissingKey));
				}
				else
				{
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.NotLocked) { Specifier = portal.DisplayName });
				}
			}
		}

		bool PlayerHasKey(LockTag lockTag)
		{
			return Game.Player.Inventory.Any(i => i.GetTag<KeyTag>()?.LockId == lockTag.LockId);
		}
	}
}
