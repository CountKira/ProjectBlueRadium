﻿namespace BusinessLogic
{
	public class Player
	{
		public ItemCollection Equipment { get; } = new ItemCollection();
		public ItemCollection Inventory { get; } = new ItemCollection();

	}
}