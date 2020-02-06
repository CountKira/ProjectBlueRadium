using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Effects;

namespace BusinessLogic
{
	public abstract class Entity
	{
		protected Entity(IEnumerable<Tag> tags )
		{
			this.tags = tags;
		}

		readonly IEnumerable<Tag> tags;
		protected Dictionary<Tag, IEffect>? effects;
		public bool HasTag(Tag tag) => tags.Contains(tag);

		public IEffect? GetEffect(Tag tag)
		{
			return effects?[tag];
		}
	}
}