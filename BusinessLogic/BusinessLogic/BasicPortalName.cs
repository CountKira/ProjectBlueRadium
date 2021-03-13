namespace BusinessLogic
{
	public static class BasicPortalName
	{
		public static PortalName North => new("north");
		public static PortalName South => new("south");
		public static PortalName West => new("west");
		public static PortalName East => new("east");
	}
}