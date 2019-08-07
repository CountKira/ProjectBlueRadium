namespace BusinessLogic.Verbs
{
	abstract class Verb
	{
		protected readonly IWriter writer;
		protected IGame game;
		public abstract void Execute(string s);
		protected Verb(IWriter writer)
		{
			this.writer = writer;
		}

		public void Initialize(IGame game) => this.game = game;
	}
}