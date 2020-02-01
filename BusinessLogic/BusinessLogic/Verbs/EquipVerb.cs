namespace BusinessLogic.Verbs
{
	class EquipVerb : Verb
	{
		public EquipVerb(IWriter writer) : base(writer) { }

		public override void Execute(string itemName)
		{
			var itemObj = Game!.GetItemFromPlayerInventory(itemName);
			if (itemObj is null)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
					{Specifier = itemName});
			}
			else
			{
				if (itemObj.HasTag(Tag.Weapon))
				{
					Game.EquipWeapon(itemObj);
					Game.HasActed();
				}
				else
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.CanNotEquip)
						{Specifier = itemObj.Name});
			}
		}
	}
}