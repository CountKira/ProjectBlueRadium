using System;
using BusinessLogic;

namespace SharedViewResources {
	public class SystemRandom : IRandom
	{
		readonly Random random = new Random();

		/// <inheritdoc />
		public int Next(int i) => random.Next(i);
	}
}