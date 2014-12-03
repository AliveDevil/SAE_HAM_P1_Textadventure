/*
 * Author: Jöran Malek
 */

namespace TextAdventure.Scenes.Components
{
	public abstract class Component
	{
		public virtual string Name { get { return this.GetType().Name; } }
	}
}
