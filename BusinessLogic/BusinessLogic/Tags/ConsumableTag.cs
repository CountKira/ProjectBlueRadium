using BusinessLogic.Effects;

namespace BusinessLogic.Tags {
	public class ConsumableTag: ITag
	{
		readonly IEffect effect;

		public ConsumableTag(IEffect effect)
		{
			this.effect = effect;
		}
		/// <inheritdoc />
		public Tag GetTag() => Tag.Consumable;

		public IEffect GetEffect() => effect;
	}
}