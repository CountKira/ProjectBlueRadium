using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLogic.Effects;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public class Item : Entity
	{
		public Item(string name, string description, IEnumerable<Tag>? tags = null, IDictionary<Tag, IEffect>? effects = null)
		: base(tags ?? Enumerable.Empty<Tag>())
		{
			Name = name;
			Description = description;

			this.effects = new Dictionary<Tag, IEffect>(effects ?? new Dictionary<Tag, IEffect>());
		}

		public string Name { get; }
		public string Description { get; }
	}
}