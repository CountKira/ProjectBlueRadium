namespace BusinessLogic.Tags
{
	public class LockTag : ITag
	{
		public LockId LockId { get; }

		public LockTag(LockId lockId)
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