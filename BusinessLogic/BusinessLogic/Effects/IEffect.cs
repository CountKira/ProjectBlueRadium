namespace BusinessLogic.Effects
{
	public interface IEffect
	{
		string ActOn(Player subject);
	}
}