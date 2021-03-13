﻿using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Passage : Entity
	{
		/// <inheritdoc />
		Passage(IEnumerable<ITag>? tags = null) : base(tags ?? Enumerable.Empty<ITag>()) { }

		public class Builder
		{
			readonly List<(RoomId, Portal.Builder)> portals = new(2);
			readonly Passage passage;

			public Builder(IEnumerable<ITag>? tags = null) => passage = new Passage(tags);

			public Passage Build() => passage;

			public void Add(Portal.Builder builder, RoomId roomId)
			{
				portals.Add((roomId, builder));
				if (portals.Count==2)
				{
					var (roomIdA, entryWayA) = portals[0];
					var (roomIdB, entryWayB) = portals[1];
					entryWayA.RoomGuid = roomIdB;
					entryWayB.RoomGuid = roomIdA;
				}
			}
		}
	}
}