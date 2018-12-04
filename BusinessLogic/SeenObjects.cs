namespace BusinessLogic
{
	public class SeenObjects
	{
		public SeenObjects(string entityDescription, Passage[] passages, ItemCollection items, Creature[] creatures)
		{
			EntityDescription = entityDescription;
			Passages = passages;
			Items = items;
			Creatures = creatures;
		}

		public string EntityDescription { get; }
		public Passage[] Passages { get; }
		public ItemCollection Items { get; }
		public Creature[] Creatures { get; }
	}
}