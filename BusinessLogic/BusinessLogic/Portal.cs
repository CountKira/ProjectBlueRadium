using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(DisplayName) + "}")]
	public class Portal
	{
		Portal(int roomGuid, string displayName, Passage passage)
		{
			Passage = passage;
			RoomGuid = roomGuid;

			DisplayName = displayName;
		}

		public int RoomGuid { get; }
		public string DisplayName { get; }
		public Passage Passage { get; }

		public class Builder
		{
			Passage.Builder Passage { get; }
			string DisplayName { get; }

			public int? RoomGuid { get; set; }

			public Builder(Passage.Builder passage, string displayName)
			{
				Passage = passage;
				DisplayName = displayName;
			}

			public Portal Build()
			{
				if (!RoomGuid.HasValue)
				{
					throw new InvalidOperationException();
				}
				return new Portal(RoomGuid.Value, DisplayName, Passage.Build());
			}

			public void ManageRoomId(int roomId)
			{
				Passage.Add(this, roomId);
			}
		}
	}
}