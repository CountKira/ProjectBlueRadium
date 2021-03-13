using BusinessLogic;
using SadConsole;

namespace SadConsoleView.Writer
{
	public class HealthPointWriter : INotificationHandler<int>
	{
		readonly Console statusConsole;

		public HealthPointWriter(Console statusConsole) => this.statusConsole = statusConsole;

		/// <inheritdoc />
		public void Notify(int e)
		{
			statusConsole.Clear();
			statusConsole.Print(0, 0, $"Hp: {e}");
		}
	}
}