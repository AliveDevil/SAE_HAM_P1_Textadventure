/*
 * Author: Jöran Malek
 */

namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// A simple enemy without difficult stats.
	/// </summary>
	public sealed class Goblin : Entity
	{
		/// <summary>
		/// Private constructor. Nothing should ever create an instance from this.
		/// </summary>
		/// <param name="name">Components name.</param>
		/// <param name="damage">Entities damage.</param>
		/// <param name="health">Entities health.</param>
		private Goblin(string name, int damage, int health)
			: base(name, true, damage, health)
		{
		}

		/// <summary>
		/// Static factory function for a small goblin.
		/// </summary>
		/// <param name="name">What is this goblins name?</param>
		/// <returns>A small goblin.</returns>
		public static Goblin SmallGoblin(string name)
		{
			return new Goblin(name, 1, 10);
		}

		/// <summary>
		/// Tries to defend against attacker.
		/// </summary>
		/// <param name="attacker">Entity attacking current entity.</param>
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
