using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	public static class ItemFactory
	{
		public static Item HealingPotion() => new("healing potion", "a healing potion",
			new[] {new ConsumableTag(new HealEffect(new(5))),});

		public static Item Dagger() => new("dagger", "a dagger", new[] {new WeaponTag(new(2)),});
		public static Item Sword() => new("sword", "a sword", new[] {new WeaponTag(new(4)),});
		public static Item Key(LockId lockId) => new("key", "a key", new[] {new KeyTag(lockId),});
	}
}