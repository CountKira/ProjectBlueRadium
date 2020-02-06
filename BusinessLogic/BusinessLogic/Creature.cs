using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
	public class Creature : Entity
	{
		public Creature(string name, string description, int healthPoints, int damage, IEnumerable<Tag>? tags = null):
			base(tags ?? Enumerable.Empty<Tag>())
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