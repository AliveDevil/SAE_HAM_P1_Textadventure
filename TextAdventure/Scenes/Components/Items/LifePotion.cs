/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	/// <summary>
	/// Increases maxhealth.
	/// </summary>
	public sealed class LifePotion : Potion
	{
		/// <summary>
		/// Default constructor for lifepotion.
		/// </summary>
		public LifePotion() : base("lifepotion") { }

		/// <summary>
		/// Increases maxhealth of caller.
		/// </summary>
		/// <param name="caller"></param>
		public override void Apply(Entity caller)
		{
			if (caller != null)
			{
				caller.IncreaseHealth(15);
			}
		}
	}
}
