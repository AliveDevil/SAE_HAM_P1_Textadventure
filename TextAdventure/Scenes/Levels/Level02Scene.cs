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
			DoorComponent door = new DoorComponent("door", false);
			door.Open += OpenDoor;
			AddComponent(door);
		}

		protected override string OnNoActionFound()
		{
			return Resources.Room2_Fail;
		}

		private bool OpenDoor(Component component)
		{
			SceneManager.LoadScene<Level03Scene>();
			return true;
		}
	}
}
