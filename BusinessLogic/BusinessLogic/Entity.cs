using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public abstract class Entity
	{
		readonly List<ITag> tags;

		protected Entity(IEnumerable<ITag> tags)
		{
			this.tags = new List<ITag>(tags);
		}

		public bool HasMarkerTag(Tag tag) => tags.Any(t => t is MarkerTag marker && marker.Tag == tag);

		public bool HasTag<TTag>() where TTag : ITag => tags.Any(v => v is TTag);

		public TTag? GetTag<TTag>() where TTag : class, ITag => (TTag)tags.FirstOrDefault(t => t is TTag);
	}
}