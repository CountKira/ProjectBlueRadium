namespace BusinessLogic.Verbs
{
	class LookVerb : Verb
	{
		public LookVerb(IWriter writer) : base(writer) { }

		public override void Execute(string entity)
		{
			var entityObj = Game!.GetLocalAvailableEntity(entity);
			switch (entityObj)
			{
				case null:
					writer.SetInvalidCommand(new InvalidCommand(InvalidCommandType.EntityNotFound)
						{Specifier = entity});
					break;
				case Item item:
					writer.WriteTextOutput(item.Description);
					break;
				case Creature creature:
					writer.WriteTextOutput(creature.Description);
					break;
			}
		}
	}
}