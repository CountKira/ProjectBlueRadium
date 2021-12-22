using BusinessLogic.SemanticTypes;

namespace BusinessLogic.Tags;

public class WeaponTag : ITag
{
	public WeaponTag(Damage damage) => Damage = damage;

	public Damage Damage { get; }
}