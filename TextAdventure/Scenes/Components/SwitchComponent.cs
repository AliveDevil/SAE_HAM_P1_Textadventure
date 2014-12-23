/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a switch.
	/// </summary>
	public sealed class SwitchComponent : Component
	{
		public event ComponentCallback Switch;

		public bool Switched { get; set; }

		public SwitchComponent(string name, bool enabled, bool switched)
			: base(name, enabled)
		{
			Switched = switched;
			RegisterCallback("use", Switch);
			RegisterCallback("turn", Switch);
			RegisterCallback("toggle", Switch);
			RegisterCallback("switch", Switch);
		}
	}
}
