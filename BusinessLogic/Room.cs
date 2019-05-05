using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Items;

namespace BusinessLogic
{
	public class Room
	{
		private readonly Creature[] creatures;
		private readonly string description;
		private readonly ItemCollection inventory;
		private readonly Passage[] passages;


		public Room(string description, ItemCollection inventory, Passage[] passages = null,
			Creature[] creatures = null)
		{
			this.description = description;
			this.inventory = inventory;
			this.passages = passages ?? new Passage[0];
			this.creatures = creatures ?? new Creature[0];
		}

		public SeenObjects GetDescription() => new SeenObjects(description, passages, inventory, creatures);

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

		public void PickUpItem(Item item, IGame game)
		{
			game.WriteAction(new ActionDTO(Verb.Get) {Specifier = item.Name});
			inventory.Remove(item);
			game.Inventory.Add(item);
		}

		public bool HasItem(string item) => inventory.HasItem(item);

		public Item GetItem(string item) => inventory.First(i => i.Name == item);

		public Creature GetCreature(string creatureName) =>
			creatures.FirstOrDefault(a => a.Name.Equals(creatureName, StringComparison.OrdinalIgnoreCase));
	}
}