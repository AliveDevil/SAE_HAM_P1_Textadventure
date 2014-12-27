/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels.Tower
{
	/// <summary>
	/// The hall into the mages room.
	/// </summary>
	public sealed class Hall : LevelScene
	{
		public override string Title { get { return Resources.Room2_Title; } }
		public override string Description { get { return Resources.Room2_Description; } }

		public Hall()
		{
			ChangeRoomComponent door = new ChangeRoomComponent("door", true);
			door.Follow += OpenDoor;
			AddComponent(door);
		}

		private void OpenDoor(object sender, ComponentEventArgs e)
		{
			SceneManager.LoadScene<MageRoom>();
			e.Handled = true;
		}
	}
}
