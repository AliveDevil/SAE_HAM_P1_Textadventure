/*
 * Author: Jöran Malek
 */

using System;

namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a door.
	/// </summary>
	public sealed class ChangeRoomComponent : Component
	{
		/// <summary>
		/// Thrown if this component is called.
		/// </summary>
		public event EventHandler<ComponentEventArgs> Follow;

		/// <summary>
		/// Single constructor for
		/// </summary>
		/// <param id="id">Current components id.</param>
		/// <param id="enabled">Is this component enabled.</param>
		public ChangeRoomComponent(string name, bool enabled, params Activator[] activators)
			: base(name, enabled, activators)
		{
			RegisterCallback("open", OnFollow);
			RegisterCallback("use", OnFollow);
			RegisterCallback("take", OnFollow);
			RegisterCallback("follow", OnFollow);
		}

		/// <summary>
		/// Callback for using.
		/// </summary>
		private void OnFollow(object sender, ComponentEventArgs e)
		{
			if (Follow != null)
			{
				Follow(sender, e);
			}
		}
	}
}
