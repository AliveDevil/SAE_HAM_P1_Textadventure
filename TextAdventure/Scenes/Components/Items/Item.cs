/*
 * Author: Jöran Malek
 */


namespace TextAdventure.Scenes.Components.Items
{
	public abstract class Item : Component
	{
		public Item(string name) : base(name, true) { }
	}
}
