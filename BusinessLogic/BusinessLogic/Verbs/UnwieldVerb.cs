namespace BusinessLogic.Verbs {
	class UnwieldVerb : Verb
	{
		public UnwieldVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(string itemName)
		{
			var itemObj = Game.GetItemFromPlayerEquipment(itemName);
			if (itemObj is null)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
					{ Specifier = itemName });
			}
			else
			{
				Game.UnwieldWeapon(itemObj);
				Game.HasActed();
			}
		}
	}
}