using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class WieldVerb : Verb
	{
		public WieldVerb(IWriter writer) : base(writer) { }

		public override void Execute(string itemName)
		{
			var itemObj = Game!.GetItemFromPlayerInventory(itemName);
			if (itemObj is null)
			{
				writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
				{ Specifier = itemName });
			}
			else
			{
				if (itemObj.HasTag<WeaponTag>())
				{
					Game.WieldWeapon(itemObj);
					Game.HasActed();
				}
				else
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.CanNotWield)
					{ Specifier = itemObj.Name });
			}
		}
	}
}