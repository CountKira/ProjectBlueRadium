using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
	public class Creature
	{
		public Creature(string name, string description, int healthPoints, int damage)
		{
			Name = name;
			Description = description;
			HealthPoints = healthPoints;
			Damage = damage;
		}

		public string Name { get; }
		public string Description { get; }
		public int HealthPoints { get; set; }
		public int Damage { get; }
	}
}