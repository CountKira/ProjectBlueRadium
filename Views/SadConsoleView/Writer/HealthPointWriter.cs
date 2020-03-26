using BusinessLogic;
using SadConsole;

namespace SadConsoleView.Writer 
{
	public class HealthPointWriter : INotificationHandler<Creature, int>
	{
		readonly Console statusConsole;

		public HealthPointWriter(Console statusConsole)
		{
			this.statusConsole = statusConsole;
		}

		/// <inheritdoc />
		public void Notify(Creature sender, int e)
		{
			statusConsole.Clear();
			statusConsole.Print(0, 0, $"Hp: {e}");
		}
	}
}