using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
	public class ItemCollection : IEnumerable<Item>
	{
		readonly List<Item> items = new();

		/// <inheritdoc />
		public IEnumerator<Item> GetEnumerator() => items.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add(Item item)
		{
			items.Add(item);
		}

		public bool Remove(Item item) => items.Remove(item);
		public bool HasItem(Item item) => items.Contains(item);

		public Item? GetItem(ItemName itemName) => items.FirstOrDefault(i => i.Name == itemName);
	}
}