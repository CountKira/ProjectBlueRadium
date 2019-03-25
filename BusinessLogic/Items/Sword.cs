using System;

namespace BusinessLogic.Items
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
			switch (verb)
			{
				case Verb.Look:
					game.WriteDescription(Description);
					break;
				case Verb.Get:
					game.PickUpItem(this);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
			}
		}
	}
}