/*
 * Author: Jöran Malek
 */

using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	public class HealthPotion : Potion
	{
		public HealthPotion() : base("healthpotion") { }

		public override void Apply(Component caller)
		{
			Entity entity = caller as Entity;
			if (entity != null)
			{
				entity.Heal(10);
			}
		}
	}
}
