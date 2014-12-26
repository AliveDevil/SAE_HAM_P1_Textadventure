/*
 * Author: Jöran Malek
 */

namespace TextAdventure.Scenes.Components.Entities
{
	public sealed class Goblin : Entity
	{
		public static Goblin SmallGoblin(string name)
		{
			return new Goblin(name, 1, 10);
		}

		private Goblin(string name, int damage, int health) : base(name, true, damage, health) { }

		protected override void ReceiveDamage(Entity attacker)
		{
			base.ReceiveDamage(attacker);
			if (!IsDead() && attacker != null)
			{
				Attack(attacker);
			}
		}
	}
}
