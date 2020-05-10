using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Creature : Entity
	{
		int healthPoints;
		readonly INotificationHandler<int>? healthPointsChanged;

		public Creature(string name,
			string description,
			int healthPoints,
			int damage,
			INotificationHandler<int>? healthPointsChanged = null,
			IEnumerable<ITag>? tags = null) :
			base(tags ?? Enumerable.Empty<ITag>())
		{
			Name = name;
			Description = description;
			MaxHealthPoints = healthPoints;
			HealthPoints = healthPoints;
			Damage = damage;
			this.healthPointsChanged = healthPointsChanged;
		}

		public string Name { get; }
		public string Description { get; }
		public int MaxHealthPoints { get; }

		public int HealthPoints
		{
			get => healthPoints;
			set
			{
				healthPoints = value;
				healthPointsChanged?.Notify(value);
			}
		}

		public int Damage { get; }

	}
}