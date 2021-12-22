namespace BusinessLogic.Verbs;

abstract class Verb
{
	protected readonly IWriter writer;

	protected Verb(IWriter writer, IGame game)
	{
		this.writer = writer;
		Game = game;
	}

	protected IGame Game { get; }

	public abstract void Execute(ExecutionTarget target);
}