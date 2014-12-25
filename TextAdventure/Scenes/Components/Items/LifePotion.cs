
/*
 * Author: Jöran Malek
 */
using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	public sealed class LifePotion : Potion
	{
		public LifePotion() : base("lifepotion") { }

		public override void Apply(Component caller)
		{
			Entity entity = caller as Entity;
			if (entity != null)
			{
				entity.IncreaseHealth(15);
			}
		}
	}
}
