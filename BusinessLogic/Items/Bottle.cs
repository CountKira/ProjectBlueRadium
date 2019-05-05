using System;

namespace BusinessLogic.Items
{
	public class Bottle : Item
	{
		private const string Desc = "This is a glass bottle, with a green substance inside it.";

		/// <inheritdoc />
		public Bottle() : base("bottle", Desc)
		{
		}

		/// <inheritdoc />
		public override void Act(Verb verb, IGame game)
		{
			switch (verb)
			{
				case Verb.Drink:
					game.YouDiedByPoison();
					game.Stop();
					break;
				case Verb.Look:
					game.WriteDescription(Description);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
			}
		}
	}
}