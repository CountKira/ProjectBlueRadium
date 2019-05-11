using System;
using BusinessLogic;

namespace SharedTestingResources.Items
{
	public class Shield : Item
	{
		public Shield() : base("shield", "A shield")
		{
		}

		/// <inheritdoc />
		public override void Act(Verb verb, IGame game)
		{
			throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
		}
	}
}