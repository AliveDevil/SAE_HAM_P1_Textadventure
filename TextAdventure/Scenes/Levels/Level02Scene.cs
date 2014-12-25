/*
 * Author: Jöran Malek
 */

using System.Collections.Generic;
using TextAdventure.Properties;
using TextAdventure.Scenes.Components;

namespace TextAdventure.Scenes.Levels
{
	/// <summary>
	/// The hall into the mages room.
	/// </summary>
	public class Level02Scene : LevelScene
	{
		public override string Title { get { return Resources.Room2_Title; } }
		public override string Description { get { return Resources.Room2_Description; } }

		public Level02Scene()
		{
			DoorComponent door = new DoorComponent("door", true);
			door.Open += OpenDoor;
			AddComponent(door);
		}

		private bool OpenDoor(Component component, string parameter)
		{
			SceneManager.LoadScene<Level03Scene>();
			return true;
		}
	}
}
