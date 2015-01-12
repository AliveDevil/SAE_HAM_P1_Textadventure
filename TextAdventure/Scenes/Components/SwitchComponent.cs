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
		/// <param id="id">Lookup id.</param>
		/// <param id="enabled">Is this component enabled?</param>
		/// <param id="switched">Is this switch switched on?</param>
		public SwitchComponent(string name, bool enabled, bool switched, params Activator[] activators)
			: base(name, enabled, activators)
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

		public override void Dispose()
		{
			Switch = null;
			base.Dispose();
		}
	}
}
