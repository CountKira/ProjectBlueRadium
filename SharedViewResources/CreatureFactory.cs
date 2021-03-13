using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources
{
	static class CreatureFactory
	{
		public static Creature Goblin(IEnumerable<Item>? inventory = null, IEnumerable<ITag>? tags = null) =>
			new("goblin", "a little green man", new(4, null), new(2), inventory, tags);
	}
}