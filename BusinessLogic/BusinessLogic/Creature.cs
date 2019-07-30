using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
	public class Creature
	{
		public Creature(string name, string description, int healthPoints)
		{
			Name = name;
			Description = description;
			HealthPoints = healthPoints;
		}

		public string Name { get; }
		public string Description { get; }
		public int HealthPoints { get; set; }
	}
}