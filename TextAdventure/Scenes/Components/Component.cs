/*
 * Author: Jöran Malek
 */

using System;
using TextAdventure.Properties;
namespace TextAdventure.Scenes.Components
{
	public abstract class Component
	{
		private Action callback;

		public virtual string Name { get { return this.GetType().Name; } }
		public virtual string Action { get { return "interact"; } }

		public Component(Action callback)
		{
			this.callback = callback;
		}

		public bool CanInteract(string action, string name)
		{
			return action.Equals(Action, StringComparison.InvariantCultureIgnoreCase) && name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
