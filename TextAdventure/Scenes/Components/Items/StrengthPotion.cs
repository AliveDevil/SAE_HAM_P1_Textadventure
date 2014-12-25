/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	public sealed class StrengthPotion : Potion
	{
		public StrengthPotion() : base("strengthpotion") { }

		public override void Apply(Component caller)
		{
			Entity entity = caller as Entity;
			if (entity != null)
			{
				entity.IncreaseDamage(4);
			}
		}
	}
}
