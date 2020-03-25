using BusinessLogic;
using BusinessLogic.Effects;
using BusinessLogic.Tags;

namespace SharedViewResources {
	static class ItemFactory
	{
		public static Item HealingPotion() => new Item("healing potion", "A healing potion",
			new[] { new ConsumableTag(new HealEffect(5)), });

		public static Item Dagger()
		{
			return new Item("dagger", "A Dagger", new[] { new MarkerTag(Tag.Weapon), });
		}
	}
}