/*
 * Author: Jöran Malek
 */


namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Base class for components with Health and Damage.
	/// </summary>
	public abstract class Entity : Component
	{
		public int Damage { get; protected set; }
		public int Health { get; protected set; }

		public Entity(string name, bool enabled, int damage, int health)
			: base(name, enabled)
		{
			this.Damage = damage;
			this.Health = health;
		}

		public void Attack(Entity enemy)
		{
			enemy.ReceiveDamage(this);
		}

		protected void ReceiveDamage(Entity attacker)
		{
			this.Health -= attacker.Damage;
		}
	}
}
