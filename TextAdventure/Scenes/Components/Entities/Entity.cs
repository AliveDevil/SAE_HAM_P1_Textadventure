/*
 * Author: Jöran Malek
 */


using TextAdventure.Properties;
namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// Base class for components with Health and Damage.
	/// </summary>
	public abstract class Entity : Component
	{
		public event ComponentCallback Died;

		public int Strength { get; protected set; }
		public int MaxHealth { get; protected set; }
		public int Health { get; protected set; }

		public Entity(string name, bool enabled, int damage, int health)
			: base(name, enabled)
		{
			this.Strength = damage;
			this.MaxHealth = health;
			this.Health = health;
		}

		public void Heal(int amount)
		{
			Health += amount;
			PostMessage(Resources.Generic_Health, Health);
		}
		public bool Attack(Entity enemy)
		{
			if (enemy != null)
			{
				enemy.ReceiveDamage(this);
				return true;
			}
			return false;
		}
		public void RaiseDamage(int amount)
		{
			Strength += amount;
			SceneManager.CurrentScene.Message(string.Format(Resources.Potion_Message, Resources.Generic_Strength, Strength));
		}
		public void RaiseHealth(int amount)
		{
			float oldAmount = MaxHealth;
			MaxHealth += amount;
			Health = Clamp(MaxHealth * (Health / oldAmount), 0, MaxHealth);
			PostMessage(Resources.Generic_MaxHealth, MaxHealth);
			PostMessage(Resources.Generic_Health, Health);
		}
		
		protected void ReceiveDamage(Entity attacker)
		{
			this.Health -= attacker.Strength;
			SceneManager.CurrentScene.Message(string.Format(Resources.Generic_GotDamage, Name, attacker.Strength, this.Health));
			CheckDeath();
		}

		private void CheckDeath()
		{
			if (this.Health <= 0)
			{
				OnDied();
			}
		}
		private void OnDied()
		{
			if (Died != null)
			{
				Died(new ComponentEventArgs(this, null));
			}
		}
		private static void PostMessage(string title, int amount)
		{
			SceneManager.CurrentScene.Message(string.Format(Resources.Potion_Message, title, amount));
		}
		private static int Clamp(float value, int min, int max)
		{
			return (int)(value > min ? value < max ? value : max : min);
		}
	}
}
