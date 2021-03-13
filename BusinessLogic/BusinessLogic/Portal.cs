using System;
using System.Diagnostics;

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
			public Builder(Passage.Builder passage, string displayName)
			{
				Passage = passage;
				DisplayName = displayName;
			}

			Passage.Builder Passage { get; }
			string DisplayName { get; }

			public RoomId? RoomGuid { get; set; }

			public Portal Build()
			{
				if (RoomGuid is null) throw new InvalidOperationException();
				return new(RoomGuid, DisplayName, Passage.Build());
			}

			public void ManageRoomId(RoomId roomId)
			{
				Passage.Add(this, roomId);
			}
		}
	}
}