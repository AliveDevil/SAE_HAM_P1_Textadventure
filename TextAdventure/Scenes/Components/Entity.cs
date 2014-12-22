/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Base class for components with Health and Damage.
	/// </summary>
	public abstract class Entity : Component
	{
		public int Damage { get; protected set; }
		public int Health { get; protected set; }

		public Entity(string name, int damage, int health, string[] activateOn, ComponentCallback callback)
			: base(name, activateOn, callback)
		{
			this.Damage = damage;
			this.Health = health;
		}

		public void Attack(Entity living)
		{
			living.ReceiveDamage(this);
		}

		protected void ReceiveDamage(Entity attacker)
		{
			this.Health -= attacker.Damage;
		}
	}
}
