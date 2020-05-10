namespace BusinessLogic.Tags
{
	public class LockTag : ITag
	{
		public int LockId { get; }

		public LockTag(int lockId)
		{
			LockId = lockId;
		}

		public bool IsLocked { get; private set; } = true;

		public void Unlock()
		{
			IsLocked = false;
		}
	}
}