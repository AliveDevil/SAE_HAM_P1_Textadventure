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
		/// <summary>
		/// Thrown if current entity died.
		/// </summary>
		public event EventHandler<ComponentEventArgs> Died;

		/// <summary>
		/// Current health of player.
		/// </summary>
		public int Health { get; protected set; }

		/// <summary>
		/// More Health than this is not allowed.
		/// </summary>
		public int MaxHealth { get; protected set; }

		/// <summary>
		/// Defines how much damage this entity can give.
		/// </summary>
		public int Strength { get; protected set; }

		/// <summary>
		/// Abstract protected constructor.
		/// </summary>
		/// <param id="id">Components id</param>
		/// <param id="enabled">Is this component enabled?</param>
		/// <param id="strength">Current strength</param>
		/// <param id="health">Current max health and health.</param>
		protected Entity(string name, bool enabled, int strength, int health)
			: base(name, enabled)
		{
			this.Strength = strength;
			this.MaxHealth = health;
			this.Health = health;
		}

		/// <summary>
		/// Attacks an entity.
		/// </summary>
		/// <param id="enemy">The enemy.</param>
		/// <returns>Whether this attack has been successful.</returns>
		public bool Attack(Entity enemy)
		{
			if (enemy != null)
			{
				enemy.ReceiveDamage(this);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Heals current entity by given amount.
		/// </summary>
		/// <param id="amount">The amount added to current health.</param>
		public void Heal(int amount)
		{
			Health = Clamp(Health + amount, 0, MaxHealth);
			PostMessage(Resources.Generic_Health, Health);
		}

		/// <summary>
		/// Inreases max health and heals previous health to maxhealth ratio.
		/// </summary>
		/// <param id="amount"></param>
		public void IncreaseHealth(int amount)
		{
			float oldAmount = MaxHealth;
			MaxHealth += amount;
			Health = Clamp(MaxHealth * (Health / oldAmount), 0, MaxHealth);
			PostMessage(Resources.Generic_MaxHealth, MaxHealth);
			PostMessage(Resources.Generic_Health, Health);
		}

		/// <summary>
		/// Increases strength by given amount.
		/// </summary>
		/// <param id="amount">Value added to strength.</param>
		public void IncreaseStrength(int amount)
		{
			Strength += amount;
			SceneManager.CurrentScene.PostMessage(CultureInfo.CurrentCulture, Resources.Potion_Message, Resources.Generic_Strength, Strength);
		}

		/// <summary>
		/// Checks whether this entity has less than or equal to zero health.
		/// </summary>
		/// <returns>If this entity is dead.</returns>
		protected bool IsDead()
		{
			return this.Health <= 0;
		}

		/// <summary>
		/// Decreases health by given attackers strength with a range from 0.5 to 1.5.
		/// </summary>
		/// <param id="attacker">The attacking entity</param>
		protected virtual void ReceiveDamage(Entity attacker)
		{
			if (attacker != null)
			{
				int damage = (int)Math.Ceiling(attacker.Strength * (SceneManager.RandomNumberGenerator.NextDouble() + 0.5));
				this.Health -= damage;
				SceneManager.CurrentScene.PostMessage(CultureInfo.CurrentCulture, Resources.Generic_GotDamage, Id, damage, this.Health);
			}
			CheckDeath();
		}

		/// <summary>
		/// Clamps given value to min and max.
		/// </summary>
		/// <param id="value">Current value.</param>
		/// <param id="min">Minimum value.</param>
		/// <param id="max">Maximum value.</param>
		/// <returns>Clamped value.</returns>
		private static int Clamp(float value, int min, int max)
		{
			return (int)(value > min ? value < max ? value : max : min);
		}

		/// <summary>
		/// Posts a mesage to current scene.
		/// </summary>
		/// <param id="title">Changed property</param>
		/// <param id="amount">Change amount.</param>
		private static void PostMessage(string title, int amount)
		{
			SceneManager.CurrentScene.PostMessage(CultureInfo.CurrentCulture, Resources.Potion_Message, title, amount);
		}

		/// <summary>
		/// Calls OnDied() if current entity is dead.
		/// </summary>
		private void CheckDeath()
		{
			if (IsDead())
			{
				OnDied();
			}
		}

		/// <summary>
		/// Calls Died-Event .
		/// </summary>
		private void OnDied()
		{
			if (Died != null)
			{
				Died(this, null);
			}
		}
	}
}
