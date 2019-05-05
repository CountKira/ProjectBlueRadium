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
		public override void Act(Verb verb, IGame game)
		{
			switch (verb)
			{
				case Verb.Look:
					game.WriteDescription(Description);
					break;
				default: throw new NotImplementedException();
			}
		}
	}
}