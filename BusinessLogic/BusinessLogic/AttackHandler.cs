using System.Linq;
using BusinessLogic.SemanticTypes;
using BusinessLogic.Tags;

namespace BusinessLogic
{
	class AttackHandler
	{
		readonly IRandom random;
		readonly Game game;
		readonly IWriter writer;

		public AttackHandler(IWriter writer, IRandom random, Game game)
		{
			this.writer = writer;
			this.random = random;
			this.game = game;
		}

		public void AttackCreature(Creature player, Creature enemy)
		{
			if (DoesMiss())
			{
				writer.WriteTextOutput("Missed.");
				return;
			}

			var damage = player.GetDamage();
			DealDamageToCreature(damage, enemy);
			if (enemy.IsDead)
			{
				writer.WriteTextOutput($"You have slain the {enemy.Name}.");

				if (enemy.HasMarkerTag(Tag.GameEnd))
				{
					writer.WriteTextOutput("You have slain the final enemy. A winner is you.");
					game.Stop();
				}
			}
		}

		public void GetAttackedByCreature(Creature player, Creature enemy)
		{
			if (DoesMiss())
			{
				writer.WriteTextOutput($"The {enemy.Name} missed his attack.");
				return;
			}

			writer.WriteTextOutput($"The {enemy.Name} attacks you and deals {player.HealthPoints.Damage(enemy.Damage)} damage.");

			if (player.IsDead)
			{
				writer.WriteTextOutput($"The {enemy.Name} killed you.");
				game.Stop();
			}
		}
		bool DoesMiss() => random.Next(2) == 0;

		void DealDamageToCreature(Damage damage, Creature creature) =>
			writer.WriteTextOutput($"You attack the {creature.Name} and deal {creature.HealthPoints.Damage(damage)} damage.");
	}
}