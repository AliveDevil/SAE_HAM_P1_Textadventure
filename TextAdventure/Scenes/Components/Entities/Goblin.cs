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
		/// <param id="id">Components id.</param>
		/// <param id="damage">Entities damage.</param>
		/// <param id="health">Entities health.</param>
		private Goblin(string name, int damage, int health, params Activator[] activators)
			: base(name, true, damage, health, activators)
		{
		}

		/// <summary>
		/// Static factory function for a medium goblin.
		/// </summary>
		/// <param id="id">Id for this entity.</param>
		/// <returns>A medium goblin with constant values.</returns>
		public static Goblin MediumGoblin(string name, params Activator[] activators)
		{
			return new Goblin(name, 21, 29, activators);
		}

		/// <summary>
		/// Static factory function for a small goblin.
		/// </summary>
		/// <param id="id">What is this goblins id?</param>
		/// <returns>A small goblin.</returns>
		public static Goblin SmallGoblin(string name, params Activator[] activators)
		{
			return new Goblin(name, 6, 13, activators);
		}

		/// <summary>
		/// Tries to defend against attacker.
		/// </summary>
		/// <param id="attacker">Entity attacking current entity.</param>
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
