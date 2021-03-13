using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(DisplayName) + "}")]
	public class Portal
	{
		Portal(RoomId roomGuid, string displayName, Passage passage)
		{
			Passage = passage;
			RoomGuid = roomGuid;

			DisplayName = displayName;
		}

		public RoomId RoomGuid { get; }
		public string DisplayName { get; }
		public Passage Passage { get; }

		public class Builder
		{
			Passage.Builder Passage { get; }
			string DisplayName { get; }

			public RoomId? RoomGuid { get; set; }

			public Builder(Passage.Builder passage, string displayName)
			{
				Passage = passage;
				DisplayName = displayName;
			}

			public Portal Build()
			{
				if (RoomGuid is null)
				{
					throw new InvalidOperationException();
				}
				return new Portal(RoomGuid, DisplayName, Passage.Build());
			}

			public void ManageRoomId(RoomId roomId)
			{
				Passage.Add(this, roomId);
			}
		}
	}
}