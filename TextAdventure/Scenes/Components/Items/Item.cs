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
		/// <summary>
		/// Basic abstract constructor
		/// </summary>
		/// <param id="id">Components id.</param>
		protected Item(string name)
			: base(name, true)
		{
		}
	}
}
