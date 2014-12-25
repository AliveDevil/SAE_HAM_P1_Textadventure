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
		public event EventHandler<ComponentEventArgs> Follow;

		public ChangeRoomComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("open", OnFollow);
			RegisterCallback("use", OnFollow);
			RegisterCallback("take", OnFollow);
			RegisterCallback("follow", OnFollow);
		}

		private void OnFollow(object sender, ComponentEventArgs e)
		{
			if (Follow != null)
			{
				Follow(sender, e);
			}
		}
	}
}
