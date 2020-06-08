namespace BusinessLogic
{
	public class Player
	{
		public static Creature GetNewInstance(INotificationHandler<int>? healthPointsChanged) => new Creature("The player", "The hero of our story", 10, 2, healthPointsChanged);
	}
}