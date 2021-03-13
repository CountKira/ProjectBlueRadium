using BusinessLogic.Effects;

namespace BusinessLogic.Tags
{
	public class ConsumableTag : ITag
	{
		readonly IEffect effect;

		public ConsumableTag(IEffect effect) => this.effect = effect;

		public IEffect GetEffect() => effect;
	}
}