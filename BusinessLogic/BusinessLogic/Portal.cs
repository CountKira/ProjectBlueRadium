using System.Diagnostics;

namespace BusinessLogic;

[DebuggerDisplay("{" + nameof(DisplayName) + "}")]
public class Portal
{
	Portal(RoomId roomGuid, PortalName displayName, Passage passage)
	{
		Passage = passage;
		RoomGuid = roomGuid;

		DisplayName = displayName;
	}

	public RoomId RoomGuid { get; }
	public PortalName DisplayName { get; }
	public Passage Passage { get; }

	public class Builder
	{
		public Builder(Passage.Builder passage, PortalName displayName)
		{
			Passage = passage;
			DisplayName = displayName;
		}

		Passage.Builder Passage { get; }
		PortalName DisplayName { get; }

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