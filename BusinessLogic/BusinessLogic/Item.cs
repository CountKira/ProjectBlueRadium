using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public class Item : Entity
	{
		public Item(ItemName name, string description, IEnumerable<ITag>? tags = null)
			: base(tags ?? Enumerable.Empty<ITag>())
		{
			Name = name;
			Description = description;
		}

		public ItemName Name { get; }
		public string Description { get; }
	}
}