namespace BusinessLogic.Verbs
{
	class DrinkVerb : Verb
	{
		/// <inheritdoc />
		public DrinkVerb(IWriter writer) : base(writer) { }

		/// <inheritdoc />
		public override void Execute(string itemName)
		{
			if (game.GetLocalAvailableEntity(itemName) is Item item)
			{
				item.Act(VerbEnum.Drink, game);
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