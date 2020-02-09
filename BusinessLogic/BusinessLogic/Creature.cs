using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Creature : Entity
	{
		public Creature(string name, string description, int healthPoints, int damage, IEnumerable<ITag>? tags = null):
			base(tags ?? Enumerable.Empty<ITag>())
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