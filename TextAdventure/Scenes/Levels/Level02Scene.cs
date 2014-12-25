/*
 * Author: Jöran Malek
 */

using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels
{
	/// <summary>
	/// The hall into the mages room.
	/// </summary>
	public sealed class Level02Scene : LevelScene
	{
		public override string Title { get { return Resources.Room2_Title; } }
		public override string Description { get { return Resources.Room2_Description; } }

		public Level02Scene()
		{
			ChangeRoomComponent door = new ChangeRoomComponent("door", true);
			door.Open += OpenDoor;
			AddComponent(door);
		}

		private bool OpenDoor(ComponentEventArgs e)
		{
			SceneManager.LoadScene<Level03Scene>();
			return true;
		}
	}
}
