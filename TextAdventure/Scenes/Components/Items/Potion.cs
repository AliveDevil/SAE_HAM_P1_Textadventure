/*
 * Author: Jöran Malek
 */

namespace TextAdventure.Scenes.Components.Items
{
	public abstract class Potion : Item
	{
		public Potion(string name) : base(name) { }

		public abstract void Apply(Component caller);
	}
}
