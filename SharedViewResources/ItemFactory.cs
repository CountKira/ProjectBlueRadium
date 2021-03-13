﻿using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	public static class ItemFactory
	{
		public static Item HealingPotion() => new Item("healing potion", "a healing potion",
			new[] { new ConsumableTag(new HealEffect(5))});

		public static Item Dagger() => new Item("dagger", "a dagger", new[] { new WeaponTag(2)});
		public static Item Sword() => new Item("sword", "a sword", new[] { new WeaponTag(4)});
		public static Item Key(LockId lockId) => new Item("key", "a key", new[] { new KeyTag(lockId)});
	}
}