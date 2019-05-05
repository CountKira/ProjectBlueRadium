﻿using System;

namespace BusinessLogic.Items
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
			switch (verb)
			{
				case Verb.Get:
					game.PickUpItem(this);
					break;
				default: throw new NotImplementedException();
			}
		}
	}
}