using System;
using BusinessLogic;

namespace SharedTestingResources
{
	public class FireballSpellBook : Item
	{
		/// <inheritdoc />
		public FireballSpellBook() : base("fireball spell book",
			"The book contains the teachings to learn the spell fireball.")
		{
		}

		/// <inheritdoc />
		public override void Act(Verb verb, IGame game)
		{
			throw new NotImplementedException();
		}
	}
}