using System;

namespace BusinessLogic.Verbs
{
	abstract class Verb
	{
		protected readonly IWriter writer;

		protected Verb(IWriter writer, IGame game)
		{
			this.writer = writer;
			Game = game;
		}

		public abstract void Execute(string s);

		protected IGame Game { get; }
	}
}