namespace BusinessLogic.Verbs
{
	class AttackVerb : Verb
	{
		/// <inheritdoc />
		public AttackVerb(IWriter writer, IGame game) : base(writer, game) { }

		/// <inheritdoc />
		public override void Execute(string enemy)
		{
			Game.HandleAttacking(enemy);
		}
	}
}