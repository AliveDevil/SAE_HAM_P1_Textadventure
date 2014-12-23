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
		public event ComponentCallback Open;

		public DoorComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("open", Open);
			RegisterCallback("use", Open);
		}
	}
}
