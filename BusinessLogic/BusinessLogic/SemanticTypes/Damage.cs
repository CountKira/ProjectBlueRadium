using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.SemanticTypes
{
	public readonly struct Damage
	{
		public Damage(int damage)
		{
			Value = damage;
		}
		public int Value { get; }

		public static implicit operator Damage(int value) => new Damage(value);
	}
}
