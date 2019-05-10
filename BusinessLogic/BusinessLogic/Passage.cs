using System.Diagnostics;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(DisplayName) + "}")]
	public class Passage
	{
		public Passage(int roomGuid, string displayName)
		{
			RoomGuid = roomGuid;
			DisplayName = displayName;
		}

		public int RoomGuid { get; }
		public string DisplayName { get; }
	}
}