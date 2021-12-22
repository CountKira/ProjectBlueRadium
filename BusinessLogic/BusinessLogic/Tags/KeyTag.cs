namespace BusinessLogic.Tags;

public class KeyTag : ITag
{
	public KeyTag(LockId lockId) => LockId = lockId;

	public LockId LockId { get; }
}