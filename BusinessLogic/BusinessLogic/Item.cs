using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLogic.Effects;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public class Item
	{
		readonly IEnumerable<Tag> tags;
		readonly Dictionary<Tag, IEffect> effects;

		public Item(string name, string description, IEnumerable<Tag>? tags = null, IDictionary<Tag, IEffect>? effects = null)
		{
			Name = name;
			Description = description;
			this.tags = tags ?? Enumerable.Empty<Tag>();

			this.effects = new Dictionary<Tag, IEffect>(effects ?? new Dictionary<Tag, IEffect>());
		}

		public string Name { get; }
		public string Description { get; }

		public bool HasTag(Tag tag) => tags.Contains(tag);

		public IEffect GetEffect(Tag tag)
		{
			return effects[tag];
		}
	}
}