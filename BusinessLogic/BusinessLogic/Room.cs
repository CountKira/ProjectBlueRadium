using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
	public class Room
	{
		readonly Creature[] creatures;
		readonly string description;
		readonly ItemCollection itemsOnFloor;
		readonly Portal[] portals;

		Room(string description, Portal[]? portals = null,
			ItemCollection? itemsOnFloor = null,
			Creature[]? creatures = null)
		{
			this.description = description;
			this.itemsOnFloor = itemsOnFloor ?? new ItemCollection();
			this.creatures = creatures ?? new Creature[0];
			this.portals = portals ?? new Portal[0];
		}

		public SeenObjects GetDescription() => new SeenObjects(description, portals, itemsOnFloor, creatures);

		public bool TryGetPortal(string portalName, out Portal portal)
		{
			portal = portals.FirstOrDefault(p => p.DisplayName == portalName);
			return portal != null;
		}

		public void RemoveItem(Item item)
		{
			if (!itemsOnFloor.Remove(item))
			{

				// ReSharper disable once LoopCanBeConvertedToQuery
				// Maybe there is a better way to that but converting to linq always
				// returns a method with an unused return value
				foreach (var creature in creatures)
				{
					if (creature.RemoveItem(item))
					{
						return;
					}
				}
			}
		}

		public Item? GetItem(string itemName)
		{
			return itemsOnFloor.GetItem(itemName) ?? FindItemOnDeadCreatures(itemName);
		}

		Item? FindItemOnDeadCreatures(string itemName)
		{
			foreach (var creature in creatures.Where(c => c.IsDead))
			{
				if (creature.HasItem(itemName, out var item))
					return item;
			}

			return null;
		}

		public Creature? GetCreature(string creatureName) =>
			creatures.FirstOrDefault(a => a.Name.Equals(creatureName, StringComparison.OrdinalIgnoreCase));
		public IEnumerable<Creature> GetCreatures() => creatures;

		public class Builder
		{
			readonly string description;
			readonly ItemCollection? itemsOnFloor;
			readonly Creature[]? creatures;
			readonly List<Portal.Builder> portalBuilders = new List<Portal.Builder>();

			public Builder(string description,
				ItemCollection? itemsOnFloor = null,
				Creature[]? creatures = null)
			{
				this.description = description;
				this.itemsOnFloor = itemsOnFloor;
				this.creatures = creatures;
			}

			public RoomId RoomId { get; set; }

			public void AddPortal(Portal.Builder portalBuilder)
			{
				portalBuilder.ManageRoomId(RoomId);
				portalBuilders.Add(portalBuilder);
			}

			public Room Build()
			{
				if (portalBuilders == null)
				{
					throw new InvalidOperationException();
				}
				var entryWays = new Portal[portalBuilders.Count];
				for (var index = 0; index < portalBuilders.Count; index++)
				{
					var entryWayBuilder = portalBuilders[index];
					entryWays[index] = entryWayBuilder.Build();
				}

				return new Room(description, entryWays, itemsOnFloor, creatures);
			}
		}
	}
}