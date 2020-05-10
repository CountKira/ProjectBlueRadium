namespace BusinessLogic
{
	public interface INotificationHandler<in TArgs>
	{
		void Notify(TArgs e);
	}
}