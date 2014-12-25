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
		public event ComponentCallback Open;

		public ChangeRoomComponent(string name, bool enabled)
			: base(name, enabled)
		{
			RegisterCallback("open", OnOpen);
			RegisterCallback("use", OnOpen);
			RegisterCallback("take", OnOpen);
		}

		private bool OnOpen(ComponentEventArgs e)
		{
			if (Open != null)
			{
				return Open(e);
			}
			return false;
		}
	}
}
