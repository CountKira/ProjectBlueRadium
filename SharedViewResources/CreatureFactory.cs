using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	static class CreatureFactory
	{
		public static Creature Goblin(IEnumerable<Item>? inventory = null, IEnumerable<ITag>? tags = null) => new Creature("goblin", "a little green man", 4, 2, inventory: inventory, tags: tags);
	}
}