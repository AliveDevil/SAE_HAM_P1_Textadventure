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
		/// <summary>
		/// Raised if this switch is used.
		/// </summary>
		public event EventHandler<ComponentEventArgs> Switch;

		/// <summary>
		/// Is this switch active?
		/// </summary>
		public bool Switched { get; set; }
		
		/// <summary>
		/// Constructor for switch.
		/// </summary>
		/// <param name="name">Lookup name.</param>
		/// <param name="enabled">Is this component enabled?</param>
		/// <param name="switched">Is this switch switched on?</param>
		public SwitchComponent(string name, bool enabled, bool switched)
			: base(name, enabled)
		{
			Switched = switched;
			RegisterCallback("use", OnSwitch);
			RegisterCallback("turn", OnSwitch);
			RegisterCallback("toggle", OnSwitch);
			RegisterCallback("switch", OnSwitch);
		}

		/// <summary>
		/// Called on use, turn, toggle and switch.
		/// </summary>
		private void OnSwitch(object sender, ComponentEventArgs e)
		{
			if (Switch != null)
			{
				Switch(sender, e);
			}
		}
	}
}
