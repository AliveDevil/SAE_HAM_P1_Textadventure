using TextAdventure.Scenes.Components.Entities;

namespace TextAdventure.Scenes.Components.Items
{
	public sealed class StrengthPotion : Potion
	{
		public StrengthPotion() : base("strengthpotion") { }

		public override void Apply(Component caller)
		{
			if (caller is Entity)
			{
				(caller as Entity).RaiseDamage(4);
			}
		}
	}
}
