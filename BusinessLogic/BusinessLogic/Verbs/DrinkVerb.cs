namespace BusinessLogic.Verbs
{
	class DrinkVerb : Verb
	{
		/// <inheritdoc />
		public DrinkVerb(IWriter writer) : base(writer) { }

		/// <inheritdoc />
		public override void Execute(string itemName)
		{
			if (Game!.GetLocalAvailableEntity(itemName) is Item item)
			{
				if (item.HasTag(Tag.Consumable))
				{
					var effect = item.GetEffect(Tag.Consumable);
					var result = effect.ActOn(Game.Player);
					writer.WriteTextOutput(result);
					if (Game.Player.IsDead())
					{
						Game.Stop();
					}
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