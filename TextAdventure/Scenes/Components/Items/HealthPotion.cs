/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	/// <summary>
	/// Heals the caller.
	/// </summary>
	public class HealthPotion : Potion
	{
		/// <summary>
		/// Default constructor for potions.
		/// </summary>
		public HealthPotion()
			: base("healthpotion")
		{
		}

		/// <summary>
		/// Applies healing effect.
		/// </summary>
		/// <param name="caller">This one should be healed.</param>
		public override void Apply(Entity caller)
		{
			if (caller != null)
			{
				caller.Heal(10);
			}
		}
	}
}
