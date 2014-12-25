using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	public class HealthPotion : Potion
	{
		public HealthPotion() : base("healthpotion") { }

		public override void Apply(Component caller)
		{
			if (caller is Entity)
			{
				(caller as Entity).Heal(10);
			}
		}
	}
}
