/*
 * Author: Jöran Malek
 */


namespace TextAdventure.Scenes.Components.Items
{
	public abstract class Item : Component
	{
		protected Item(string name) : base(name, true) { }
	}
}
