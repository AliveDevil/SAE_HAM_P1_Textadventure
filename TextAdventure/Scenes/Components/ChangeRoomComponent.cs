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
		/// <param name="name">Current components name.</param>
		/// <param name="enabled">Is this component enabled.</param>
		public ChangeRoomComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("use", OnFollow);
			RegisterCallback("take", OnFollow);
			RegisterCallback("open", OnFollow);
			RegisterCallback("enter", OnFollow);
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
