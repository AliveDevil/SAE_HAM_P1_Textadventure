/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	public abstract class LivingComponent : Component
	{
		public int Damage { get; protected set; }
		public int Health { get; protected set; }

		public LivingComponent(string name, int damage, int health, string[] activateOn, ComponentCallback callback)
			: base(name, activateOn, callback)
		{
			this.Damage = damage;
			this.Health = health;
		}

		public void Attack(LivingComponent living)
		{
			living.ReceiveDamage(this);
		}

		protected void ReceiveDamage(LivingComponent attacker)
		{
			this.Health -= attacker.Damage;
		}
	}
}
