namespace BusinessLogic.Tags {
	public class MarkerTag : ITag
	{
		readonly Tag tag;

		public MarkerTag(Tag tag)
		{
			this.tag = tag;
		}
		/// <inheritdoc />
		public Tag GetTag() => tag;
	}
}