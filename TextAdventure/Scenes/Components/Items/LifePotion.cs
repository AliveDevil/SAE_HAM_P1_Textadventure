using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	public sealed class LifePotion : Potion
	{
		public LifePotion() : base("lifepotion") { }

		public override void Apply(Component caller)
		{
			if (caller is Entity)
			{
				(caller as Entity).RaiseHealth(15);
			}
		}
	}
}
