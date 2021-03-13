namespace BusinessLogic.Tags
{
	public class MarkerTag : ITag
	{
		public MarkerTag(Tag tag) => Tag = tag;

		public Tag Tag { get; }
	}
}