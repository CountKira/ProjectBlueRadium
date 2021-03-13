namespace BusinessLogic.Verbs
{
	class LookVerb : Verb
	{
		public LookVerb(IWriter writer, IGame game) : base(writer, game) { }

		public override void Execute(string entity)
		{
			var entityObj = Game.GetLocalAvailableEntityDescription(entity);
			if (entityObj == null)
				writer.SetInvalidCommand(new(InvalidCommandType.EntityNotFound)
					{Specifier = entity,});
			else
				writer.WriteTextOutput(entityObj);
		}
	}
}