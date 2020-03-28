using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	static class ItemFactory
	{
		public static Item HealingPotion() => new Item("healing potion", "A healing potion",
			new[] { new ConsumableTag(new HealEffect(5)), });

		public static Item Dagger() => new Item("dagger", "A dagger", new[] { new WeaponTag(2), });
		public static Item Sword() => new Item("sword", "A sword", new[] { new WeaponTag(4), });
	}
}