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

		public void AttackCreature(Player player, Creature creature)
		{
			if (DoesMiss())
			{
				writer.WriteTextOutput("Missed.");
				return;
			}

			var weapon = player.Equipment.FirstOrDefault(i => i.HasTag<WeaponTag>());
			var damage = weapon?.GetTag<WeaponTag>()?.Damage ?? 1;
			DealDamageToCreature(damage, creature);
			if (creature.IsDead)
			{
				writer.WriteTextOutput($"You have slain the {creature.Name}.");

				if (creature.HasMarkerTag(Tag.GameEnd))
				{
					writer.WriteTextOutput("You have slain the final enemy. A winner is you.");
					game.IsRunning = false;
				}
			}
		}

		public void GetAttackedByCreature(Player player, Creature creature)
		{
			if (DoesMiss())
			{
				writer.WriteTextOutput($"The {creature.Name} missed his attack.");
				return;
			}

			writer.WriteTextOutput($"The {creature.Name} attacks you and deals {player.HealthPoints.Damage(creature.Damage)} damage.");

			if (player.IsDead)
			{
				writer.WriteTextOutput($"The {creature.Name} killed you.");
				game.IsRunning = false;
			}
		}
		bool DoesMiss() => random.Next(2) == 0;

		void DealDamageToCreature(Damage damage, Creature creature) =>
			writer.WriteTextOutput($"You attack the {creature.Name} and deal {creature.HealthPoints.Damage(damage)} damage.");
	}
}