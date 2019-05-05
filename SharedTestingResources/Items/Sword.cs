using System;
using BusinessLogic;

namespace SharedTestingResources.Items
{
	public class Sword : Item
	{
		/// <inheritdoc />
		public Sword() : base("sword", "A sharp sword.")
		{
		}

		/// <inheritdoc />
		public override void Act(Verb verb, IGame game)
		{
			throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
		}
	}
}