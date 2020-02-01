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
		readonly Passage[] passages;


		public Room(string description, Passage[]? passages = null,
			ItemCollection? itemsOnFloor = null,
			Creature[]? creatures = null)
		{
			this.description = description;
			this.itemsOnFloor = itemsOnFloor ?? new ItemCollection();
			this.passages = passages ?? new Passage[0];
			this.creatures = creatures ?? new Creature[0];
		}

		public SeenObjects GetDescription() => new SeenObjects(description, passages, itemsOnFloor, creatures);

		public bool TryGetRoom(string roomName, out int i)
		{
			var o = passages.FirstOrDefault(p => p.DisplayName == roomName);
			if (o != null)
			{
				i = o.RoomGuid;
				return true;
			}

			i = -1;
			return false;
		}

		public void RemoveItem(Item item)
		{
			itemsOnFloor.Remove(item);
		}

		public bool HasItem(string item) => itemsOnFloor.HasItem(item);

		public Item GetItem(string item) =>
			itemsOnFloor.First(i => i.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

		public Creature GetCreature(string creatureName) =>
			creatures.FirstOrDefault(a => a.Name.Equals(creatureName, StringComparison.OrdinalIgnoreCase));
		public IEnumerable<Creature> GetCreatures() => creatures;
	}
}