/*
 * Author: Jöran Malek
 */

namespace TextAdventure.Scenes.Components.Items
{
	/// <summary>
	/// Some thing one can put into some inventory.
	/// </summary>
	public abstract class Item : Component
	{
		protected Item(string name) : base(name, true) { }
	}
}
