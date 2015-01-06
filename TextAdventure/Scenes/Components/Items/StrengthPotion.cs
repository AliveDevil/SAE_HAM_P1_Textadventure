/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	/// <summary>
	/// Increases entities strength.
	/// </summary>
	public sealed class StrengthPotion : Potion
	{
		/// <summary>
		/// Default constructor for strengthpotion.
		/// </summary>
		public StrengthPotion()
			: base("strengthpotion")
		{
		}

		/// <summary>
		/// Increases strength of caller.
		/// </summary>
		/// <param id="caller">Some caller entity.</param>
		public override void Apply(Entity caller)
		{
			if (caller != null)
			{
				caller.IncreaseStrength(4);
			}
		}
	}
}
