namespace BusinessLogic.Tags
{
	public class KeyTag : ITag
	{
		public KeyTag(int lockId)
		{
			LockId = lockId;
		}
		public int LockId { get; }
	}
}