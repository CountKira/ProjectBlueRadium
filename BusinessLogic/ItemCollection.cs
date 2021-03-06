﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
	public class ItemCollection : IEnumerable<Item>
	{
		private readonly List<Item> items = new List<Item>();

		/// <inheritdoc />
		public IEnumerator<Item> GetEnumerator() => items.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Add(Item item)
		{
			items.Add(item);
		}

		public void Remove(Item item) => items.Remove(item);
		public bool HasItem(string item) => items.Select(i => i.Name).Contains(item, StringComparer.OrdinalIgnoreCase);
	}
}