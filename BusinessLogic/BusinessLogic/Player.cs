using BusinessLogic.SemanticTypes;

namespace BusinessLogic
{
	public class Player : Creature
	{
		/// <inheritdoc />
		public Player(INotificationHandler<int>? healthPointsChanged = null) :
			base("The player", "The hero of our story", 10, 2, healthPointsChanged)
		{ }
	}
}