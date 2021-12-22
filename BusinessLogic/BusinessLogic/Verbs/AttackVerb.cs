namespace BusinessLogic.Verbs;

class AttackVerb : Verb
{
	/// <inheritdoc />
	public AttackVerb(IWriter writer, IGame game) : base(writer, game) { }

	/// <inheritdoc />
	public override void Execute(ExecutionTarget enemy)
	{
		Game.HandleAttacking(CreatureName.FromExecutionTarget(enemy));
	}
}