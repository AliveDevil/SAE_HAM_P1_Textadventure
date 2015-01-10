/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a takeable component.
	/// </summary>
	public sealed class TakeableComponent : Component
	{
		/// <summary>
		/// Thrown if this component is called.
		/// </summary>
		public event EventHandler<ComponentEventArgs> Interact;

		/// <summary>
		/// Single constructor for
		/// </summary>
		/// <param id="id">Current components id.</param>
		/// <param id="enabled">Is this component enabled.</param>
		public TakeableComponent(string name, bool enabled, params Activator[] activators)
			: base(name, enabled, activators)
		{
			RegisterCallback("open", OnInteract);
			RegisterCallback("use", OnInteract);
			RegisterCallback("take", OnInteract);
		}

		/// <summary>
		/// Callback for using.
		/// </summary>
		private void OnInteract(object sender, ComponentEventArgs e)
		{
			if (Interact != null)
			{
				Interact(sender, e);
			}
		}
	}
}
