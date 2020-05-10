using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class DrinkVerb : Verb
	{
		/// <inheritdoc />
		public DrinkVerb(IWriter writer) : base(writer) { }

		/// <inheritdoc />
		public override void Execute(string itemName)
		{
			var player = Game!.Player;
			if (player.Inventory.HasItem(itemName) && player.Inventory.GetItem(itemName) is { } item)
			{
				if (item.HasTag<ConsumableTag>())
				{
					Game.Player.Inventory.Remove(item);
					var tag = item.GetTag<ConsumableTag>();
					var effect = tag?.GetEffect();
					var result = effect?.ActOn(Game.Player);
					if (result != null)
						writer.WriteTextOutput(result);
					if (Game.Player.IsDead())
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
					Specifier = itemName
				};
				writer.SetInvalidCommand(invalidCommand);
			}
		}
	}
}