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
		public event EventHandler<ComponentEventArgs> Switch;

		public bool Switched { get; set; }

		public SwitchComponent(string name, bool enabled, bool switched)
			: base(name, enabled)
		{
			Switched = switched;
			RegisterCallback("use", OnSwitch);
			RegisterCallback("turn", OnSwitch);
			RegisterCallback("toggle", OnSwitch);
			RegisterCallback("switch", OnSwitch);
		}

		private void OnSwitch(object sender, ComponentEventArgs e)
		{
			if (Switch != null)
			{
				Switch(sender, e);
			}
		}
	}
}
