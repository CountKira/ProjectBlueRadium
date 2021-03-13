namespace BusinessLogic.Tags
{
	public class LockTag : ITag
	{
		public LockTag(LockId lockId) => LockId = lockId;

		public LockId LockId { get; }

		public bool IsLocked { get; private set; } = true;

		public void Unlock()
		{
			IsLocked = false;
		}
	}
}