using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class DrinkVerb : Verb
	{
		/// <inheritdoc />
		public DrinkVerb(IWriter writer, IGame game) : base(writer, game) { }

		/// <inheritdoc />
		public override void Execute(string itemName)
		{
			var player = Game.Player;
			if (player.Inventory.GetItem(itemName) is { } item)
			{
				if (item.GetTag<ConsumableTag>() is { } tag)
				{
					Game.Player.Inventory.Remove(item);
					var effect = tag?.GetEffect();
					var result = effect?.ActOn(Game.Player);
					if (result != null)
						writer.WriteTextOutput(result);
					if (Game.Player.IsDead)
						Game.Stop();
					Game.HasActed();
				}
				else
				{
					var invalidCommand = new InvalidCommand(InvalidCommandType.NotExecutable) { Specifier = itemName };
					writer.SetInvalidCommand(invalidCommand);
				}
			}
			else
			{
				var invalidCommand = new InvalidCommand(InvalidCommandType.ItemNotFound)
				{
					Specifier = itemName,
				};
				writer.SetInvalidCommand(invalidCommand);
			}
		}
	}
}