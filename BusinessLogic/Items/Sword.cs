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
		public override void Act(Verb verb)
		{
			switch (verb)
			{
				case Verb.Look:
					Game.WriteDescription(Description);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
			}
		}
	}
}