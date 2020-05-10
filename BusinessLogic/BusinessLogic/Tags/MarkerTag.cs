namespace BusinessLogic.Tags
{
	public class MarkerTag : ITag
	{
		public Tag Tag { get; }

		public MarkerTag(Tag tag)
		{
			Tag = tag;
		}
	}
}