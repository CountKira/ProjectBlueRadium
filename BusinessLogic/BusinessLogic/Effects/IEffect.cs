namespace BusinessLogic.Effects;

public interface IEffect
{
	string ActOn(Creature subject);
}