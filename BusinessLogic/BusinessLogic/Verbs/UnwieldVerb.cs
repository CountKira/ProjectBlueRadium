namespace BusinessLogic.Verbs
{
	class UnwieldVerb : Verb
	{
		public UnwieldVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(string itemName)
		{
			if (Game.UnwieldWeapon(itemName))
				Game.HasActed();
		}
	}
}