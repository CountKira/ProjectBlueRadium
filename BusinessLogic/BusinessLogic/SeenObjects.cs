namespace BusinessLogic;

public class SeenObjects
{
	public SeenObjects(string entityDescription, Portal[] portals, ItemCollection items, Creature[] creatures)
	{
		EntityDescription = entityDescription;
		Portals = portals;
		Items = items;
		Creatures = creatures;
	}

	public string EntityDescription { get; }
	public Portal[] Portals { get; }
	public ItemCollection Items { get; }
	public Creature[] Creatures { get; }
}