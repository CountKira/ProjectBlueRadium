using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
	public class Creature
	{
		public Creature(string name, string description)
		{
			Name = name;
			Description = description;
		}

		public string Name { get; }
		public string Description { get; }
	}
}