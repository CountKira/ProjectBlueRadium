using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public abstract class Entity
	{
		protected Entity(IEnumerable<ITag> tags)
		{
			foreach (var tag in tags)
			{
				this.tags.Add(tag.GetTag(), tag);
			}
		}

		protected Dictionary<Tag, ITag> tags = new Dictionary<Tag, ITag>();
		public bool HasTag(Tag tag) => tags.ContainsKey(tag);

		public T? GetTag<T>(Tag tag) where T : class, ITag
		{
			if (HasTag(tag))
			{
				return (T)tags[tag];
			}

			return null;
		}
	}
}