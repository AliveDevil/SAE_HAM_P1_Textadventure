/*
 * Author: Jöran Malek
 */


namespace TextAdventure.Scenes.Components
{
	/// <summary>
	/// Represents a door.
	/// </summary>
	public sealed class ChangeRoomComponent : Component
	{
		public event ComponentCallback Follow;

		public ChangeRoomComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("open", OnFollow);
			RegisterCallback("use", OnFollow);
			RegisterCallback("take", OnFollow);
			RegisterCallback("follow", OnFollow);
		}

		private bool OnFollow(ComponentEventArgs e)
		{
			if (Follow != null)
			{
				return Follow(e);
			}
			return false;
		}
	}
}
