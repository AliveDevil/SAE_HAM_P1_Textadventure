/*
 * Author: Jöran Malek
 */

using System;
using System.Globalization;
using TextAdventure.Properties;

namespace TextAdventure.Scenes.Components.Entities
{
	/// <summary>
	/// Base class for components with Health and Damage.
	/// </summary>
	public abstract class Entity : Component
	{
		public event EventHandler<ComponentEventArgs> Died;

		public int Strength { get; protected set; }
		public int MaxHealth { get; protected set; }
		public int Health { get; protected set; }

		protected Entity(string name, bool enabled, int strength, int health)
			: base(name, enabled)
		{
			this.Strength = strength;
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
		public void IncreaseStrength(int amount)
		{
			Strength += amount;
			SceneManager.CurrentScene.AddMessage(string.Format(CultureInfo.CurrentCulture, Resources.Potion_Message, Resources.Generic_Strength, Strength));
		}
		public void IncreaseHealth(int amount)
		{
			float oldAmount = MaxHealth;
			MaxHealth += amount;
			Health = Clamp(MaxHealth * (Health / oldAmount), 0, MaxHealth);
			PostMessage(Resources.Generic_MaxHealth, MaxHealth);
			PostMessage(Resources.Generic_Health, Health);
		}

		protected virtual void ReceiveDamage(Entity attacker)
		{
			if (attacker != null)
			{
				int damage = (int)Math.Ceiling(attacker.Strength * (SceneManager.RandomNumberGenerator.NextDouble() + 0.5));
				this.Health -= damage;
				SceneManager.CurrentScene.AddMessage(string.Format(CultureInfo.CurrentCulture, Resources.Generic_GotDamage, Name, damage, this.Health));
			}
			CheckDeath();
		}
		protected bool IsDead()
		{
			return this.Health <= 0;
		}

		private void CheckDeath()
		{
			if (IsDead())
			{
				OnDied();
			}
		}
		private void OnDied()
		{
			if (Died != null)
			{
				Died(this, null);
			}
		}

		private static void PostMessage(string title, int amount)
		{
			SceneManager.CurrentScene.AddMessage(string.Format(CultureInfo.CurrentCulture, Resources.Potion_Message, title, amount));
		}
		private static int Clamp(float value, int min, int max)
		{
			return (int)(value > min ? value < max ? value : max : min);
		}
	}
}
