using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(DisplayName) + "}")]
	public class Passage : Entity
	{
		public Passage(int roomGuid, string displayName, IEnumerable<ITag>? tags = null)
			: base(tags ?? Enumerable.Empty<ITag>())
		{
			RoomGuid = roomGuid;
			DisplayName = displayName;
		}

		public int RoomGuid { get; }
		public string DisplayName { get; }
	}
}