using System;
using BusinessLogic;

namespace SharedTestingResources.Items
{
	public class Bottle : Item
	{
		private const string Desc = "This is a glass bottle, with a green substance inside it.";

		/// <inheritdoc />
		public Bottle() : base("bottle", Desc, new[] { Tag.Consumable }) { }

		/// <inheritdoc />
	}
}