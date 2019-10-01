using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public abstract class Item
	{
		private readonly IEnumerable<string> tags;

		protected Item(string name, string description, IEnumerable<string>? tags = null)
		{
			Name = name;
			Description = description;
			this.tags = tags ?? Enumerable.Empty<string>();
		}

		public string Name { get; }
		public string Description { get; }

		public abstract void Act(VerbEnum verb, IGame game);

		public bool HasTag(string tag) => tags.Contains(tag);
	}
}