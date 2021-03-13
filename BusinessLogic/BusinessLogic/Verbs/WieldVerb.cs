using BusinessLogic.Tags;

namespace BusinessLogic.Verbs
{
	class WieldVerb : Verb
	{
		public WieldVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(string itemName)
		{
			var itemObj = Game.GetItemFromPlayerInventory(itemName);
			if (itemObj is null)
			{
				writer.SetInvalidCommand(new(InvalidCommandType.EntityNotFound)
					{Specifier = itemName,});
			}
			else
			{
				if (itemObj.HasTag<WeaponTag>())
				{
					Game.WieldWeapon(itemObj);
					Game.HasActed();
				}
				else
				{
					writer.SetInvalidCommand(new(InvalidCommandType.CanNotWield)
						{Specifier = itemObj.Name,});
				}
			}
		}
	}
}