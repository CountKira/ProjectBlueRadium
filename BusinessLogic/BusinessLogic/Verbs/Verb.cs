using System;

namespace BusinessLogic.Verbs
{
	abstract class Verb
	{
		protected readonly IWriter writer;
		IGame? game;

		protected Verb(IWriter writer)
		{
			this.writer = writer;
		}

		public abstract void Execute(string s);

		public void Initialize(IGame game) => this.game = game;
		protected IGame Game => game ?? throw new InvalidOperationException("Verb was not initialized");
	}
}