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
		public override void Act(Verb verb)
		{
			switch (verb)
			{
				case Verb.Drink:
					game.YouDiedByPoison();
					game.IsRunning = false;
					break;
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