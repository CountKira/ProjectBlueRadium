using System;

namespace BusinessLogic.Items
{
	public class Book : Item
	{
		/// <inheritdoc />
		public Book() : base("book", "The book contains the story of boatmurdered.")
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
				case Verb.Get:
					Game.PickUpItem(this);
					break;
				default: throw new NotImplementedException();
			}

		}
	}
}