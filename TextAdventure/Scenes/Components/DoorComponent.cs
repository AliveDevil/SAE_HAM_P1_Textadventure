/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a door.
	/// </summary>
	public sealed class DoorComponent : Component
	{
		public ComponentCallback Open;

		public DoorComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("open", OnOpen);
			RegisterCallback("use", OnOpen);
		}

		private bool OnOpen(Component component, string parameter)
		{
			if (Open != null)
			{
				return Open(component, parameter);
			}
			return false;
		}
	}
}
