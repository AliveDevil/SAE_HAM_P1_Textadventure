/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	public class ComponentEventArgs : EventArgs
	{
		public Component Component { get; private set; }
		public string Parameter { get; private set; }

		public ComponentEventArgs(Component component, string parameter)
		{
			Component = component;
			Parameter = parameter;
		}
	}
}
