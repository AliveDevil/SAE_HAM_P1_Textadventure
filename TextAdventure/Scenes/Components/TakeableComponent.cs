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
		/// Thrown if this component is called. Should be on this instance. Base "Interact" is method.
		/// </summary>
		public new event EventHandler<ComponentEventArgs> Interact;

		/// <summary>
		/// Single constructor for
		/// </summary>
		/// <param id="id">Current components id.</param>
		/// <param id="enabled">Is this component enabled.</param>
		public TakeableComponent(string name, bool enabled)
			: base(name, enabled)
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
