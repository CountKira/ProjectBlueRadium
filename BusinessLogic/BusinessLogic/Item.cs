﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public class Item
	{
		readonly IEnumerable<Tag> tags;

		public Item(string name, string description, IEnumerable<Tag>? tags = null)
		{
			Name = name;
			Description = description;
			this.tags = tags ?? Enumerable.Empty<Tag>();
		}

		public string Name { get; }
		public string Description { get; }

		public bool HasTag(Tag tag) => tags.Contains(tag);
	}
}