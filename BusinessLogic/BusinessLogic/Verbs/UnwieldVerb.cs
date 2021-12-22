namespace BusinessLogic.Verbs;

class UnwieldVerb : Verb
{
	public UnwieldVerb(IWriter writer, IGame game) : base(writer, game) { }

	public override void Execute(ExecutionTarget itemName)
	{
		if (Game.UnwieldWeapon(ItemName.FromExecutionTarget(itemName)))
			Game.HasActed();
	}
}