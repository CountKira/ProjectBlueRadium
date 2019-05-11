using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic;

namespace SharedTestingResources.Items
{
	class SimpleItem : Item
	{
		/// <inheritdoc />
		public SimpleItem(string name, string description) : base(name, description)
		{
		}

		/// <inheritdoc />
		public override void Act(Verb verb, IGame game)
		{
			throw new NotImplementedException();
		}
	}
}
