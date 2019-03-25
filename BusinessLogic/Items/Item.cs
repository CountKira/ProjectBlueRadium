﻿using System;
using System.Diagnostics;

namespace BusinessLogic.Items
{
	[DebuggerDisplay("{" + nameof(Name) + "}")]
	public abstract class Item
	{
		protected Item(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public string Name { get; }
		protected string Description { get; }

		public abstract void Act(Verb verb, Game game);
	}
}