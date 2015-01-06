/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	/// <summary>
	/// Abstract base class for potions.
	/// </summary>
	public abstract class Potion : Item
	{
		/// <summary>
		/// Some constructor for potions. Just passes on the id.
		/// </summary>
		/// <param id="id">Potions id.</param>
		protected Potion(string name)
			: base(name)
		{
		}

		/// <summary>
		/// Applies to entities only.
		/// </summary>
		/// <param id="caller">The caller should get current potions effect.</param>
		public abstract void Apply(Entity caller);
	}
}
