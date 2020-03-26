namespace BusinessLogic
{
	public interface INotificationHandler<in TSender, in TArgs>
	{
		void Notify(TSender sender, TArgs e);
	}
}