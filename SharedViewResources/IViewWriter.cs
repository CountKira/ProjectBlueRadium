namespace SharedViewResources
{
	public interface IViewWriter
	{
		void WriteLine(string text);
		void WriteCommand(string text);
	}
}