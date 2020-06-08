using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.SemanticTypes;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	public class Creature : Entity
	{
		public Creature(string name,
			string description,
			int healthPoints,
			Damage damage,
			INotificationHandler<int>? healthPointsChanged = null,
			IEnumerable<Item>? inventory = null,
			IEnumerable<ITag>? tags = null) :
			base(tags ?? Enumerable.Empty<ITag>())
		{
			Name = name;
			Description = description;
			HealthPoints = new HealthPoints(healthPoints, healthPointsChanged);
			Damage = damage;
			if (inventory != null)
			{
				foreach (var item in inventory)
				{
					Inventory.Add(item);
				}
			}
		}

		public string Name { get; }
		public string Description { get; }

		public bool IsDead => HealthPoints.ZeroOrBelow;

		public ItemCollection Equipment { get; } = new ItemCollection();
		public ItemCollection Inventory { get; } = new ItemCollection();

		public HealthPoints HealthPoints { get; }

		public Damage Damage { get; }

	}
}