/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	public class ComponentEventArgs : EventArgs
	{
		public bool Handled { get; set; }
		public string Parameter { get; private set; }

		public ComponentEventArgs(string parameter)
		{
			Handled = false;
			Parameter = parameter;
		}
	}
}
