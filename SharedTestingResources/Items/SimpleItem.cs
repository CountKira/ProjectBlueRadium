using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;

namespace SharedTestingResources.Items
{
	class SimpleItem : Item
	{
		/// <inheritdoc />
		public SimpleItem(string name, string description, IEnumerable<Tag> tags = null) : base(name, description,
			tags) { }
	}
}