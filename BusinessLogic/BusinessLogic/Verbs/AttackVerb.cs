namespace BusinessLogic.Verbs
{
	class AttackVerb : Verb
	{
		/// <inheritdoc />
		public AttackVerb(IWriter writer) : base(writer) { }

		/// <inheritdoc />
		public override void Execute(string enemy)
		{
			Game!.HandleAttackingTheEvilGuy(enemy);
			Game.HasActed();
		}
	}
}