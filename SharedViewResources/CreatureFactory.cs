using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Tags;

namespace SharedViewResources {
	static class CreatureFactory
	{
		public static Creature Goblin(IEnumerable<ITag>? tags = null) => new Creature("goblin", "a little green man", 4, 2, tags: tags);
	}
}